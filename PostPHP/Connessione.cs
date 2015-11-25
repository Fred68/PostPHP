using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;						// Per timer
using System.Security.Cryptography;			// Per AES (chiave simmetrica)
using System.IO;							// Per memory stream
using System.Collections.Specialized;		// Per Name Value Collection
using System.Net;							// Per UploadValuesCompletedEventHandler e altri

namespace PostPHP
	{
	class PHPanswer						// Dati scambiati per una richiesta
		{
		public string msg;							// Messaggio inviato
		public string rsp;							// Messaggio ricevuto
		public List<string> errList = null;			// Lista dei messaggi di errore
		public PHPanswer()
			{
			errList = new List<string>();
			msg = "";
			rsp = "";
			}
		}

	class Connessione
		{
		#region COSTANTI
		const int max_timer = 600;			// Valori accettabili per il refresh timer
		const int min_timer = 10;
		const int default_timer = 30;
		const string sep = "*";				// Separatore dell'IV (usa carattere esterno al set base 64)
		#endregion
		#region VARIABILI PRIVATE
		PHPanswer phpmsg = null;					// Dati scambiati per richiesta sincrona
		Queue<PHPanswer> phpmsgqueue;				// Coda delle risposte ricevute da chiamate asincrone
		System.Timers.Timer refreshTimer = null;	// Timer per il refresh	
		WebClientWithCookies client = null;			// Classe per la connessione
		string user = "";							// Utente per il login della sessione
		string password = "";						// Password per il login della sessione
		string key = new string('-', 32);			// Chiave AES per scambio dati
		string url = "";
		#endregion
		#region PROPRIETÀ PUBBLICHE
		public int WaitingMessages							// Messaggi nella coda
			{
			get { return phpmsgqueue.Count;}
			}
		public string Message								// Stringa con il messaggio da inviare
			{
			get { return phpmsg.msg; }
			set { phpmsg.msg = value; }
			}
		public string Response								// Stringa con il messaggio di risposta
			{
			get { return phpmsg.rsp; }
			}
		public List<string> ErrorList						// Lista con gli errori
			{
			get { return phpmsg.errList; }
			}
		public int Errors									// Numero di errori
			{
			get { return phpmsg.errList.Count; }
			}
		public string ErrorMessages							// Stringa con i messaggi di errore
			{
			get
				{
				StringBuilder strb = new StringBuilder();
				foreach(string str in phpmsg.errList)
					strb.Append(str + '\n');
				return strb.ToString();
				}
			}
		public void ResetMessagesAndErrors()			// Azzera tutti i messaggi
			{
			ResetError();
			ResetMessage();
			}
		public void ResetMessage()							// Azzera messaggi inviato e ricevuto
			{
			phpmsg.msg = "";
			phpmsg.rsp = "";
			}
		public void ResetError()							// Azzera messaggio di errore
			{
			phpmsg.errList.Clear();
			}
		public void AddErrorMessage(string errorMessage)	// Aggiunge messaggio di errore
			{
			phpmsg.errList.Add(errorMessage);
			}
		public string CommandUrl							// Indirizzo della pagina php di comando
			{
			get { return this.url;}
			set { this.url = value;}
			}
		#endregion
		#region COSTRUTTORI E IMPOSTAZIONE
		public Connessione()														// Costruttore
			{
			ImpostaParametri("", "", "", "", default_timer);
			}
		public Connessione(string user, string password, string key, string url, int timer_sec=default_timer)	// Costruttore
			{
			ImpostaParametri(user, password, key, url, timer_sec);
			}
		public void ImpostaParametri(string user,string password,string key,string url="",int timer_sec=default_timer)	// Inizializza valori
			{
			int msec = max_timer * 1000;
			if((timer_sec >= min_timer) && (timer_sec<= max_timer))
				{
				msec = timer_sec * 1000;
				}
			this.refreshTimer = new System.Timers.Timer(msec);
			this.refreshTimer.Elapsed += OnTimedEvent;
			this.user = user;
			this.password = password;
			this.key = key;
			this.CommandUrl = url;
			phpmsg = new PHPanswer();
			phpmsgqueue = new Queue<PHPanswer>();
			this.client = new WebClientWithCookies();
			this.client.UploadValuesCompleted += new UploadValuesCompletedEventHandler(OnPHPcmdDone);	// Hander se esecuzione completa
			}
		#endregion
		#region FUNZIONI PROTETTE
		protected void OnTimedEvent(Object source, ElapsedEventArgs e)				// Per refresh
			{
			EseguiRefresh();														// Esegue un refresh
			}
		protected string EncryptMsg(string txt)										// Codifica messaggio con aes in base64
			{
			string enc = "";
			RijndaelManaged aes = new RijndaelManaged();
			aes.KeySize = 256;
			aes.BlockSize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Mode = CipherMode.CBC;
			try
				{
				aes.Key = Convert.FromBase64String(this.key);		// Attenzione: la chiave deve essere di lunghezza corretta ! 256bit 32caratteri
				aes.GenerateIV();
				string iv = Convert.ToBase64String(aes.IV);
				var encrypt = aes.CreateEncryptor(aes.Key, aes.IV);
				byte[] xBuff = null;
				using(var ms = new MemoryStream())
					{
					try
						{
						using(var cs = new CryptoStream(ms, encrypt, CryptoStreamMode.Write))
							{
							byte[] xXml = Encoding.UTF8.GetBytes(txt);
							cs.Write(xXml, 0, xXml.Length);
							}
						}
					catch(Exception ex)
						{
						AddErrorMessage(ex.Message);
						}
					xBuff = ms.ToArray();
					}
				enc = Convert.ToBase64String(xBuff);
				enc += sep + iv;
				}
			catch(Exception ex)
				{
				AddErrorMessage(ex.Message);
				}
			return enc;
			}
		protected string DecryptMsg(string txt, ref bool ok)						// Decodifica messaggio con aes da base64
			{																		// usa bool in reference contro conflitti se chiamate asincrone
			string dec = "";
			RijndaelManaged aes = new RijndaelManaged();
			aes.KeySize = 256;
			aes.BlockSize = 256;
			aes.Padding = PaddingMode.PKCS7;
			aes.Mode = CipherMode.CBC;
			try
				{
				aes.Key = Convert.FromBase64String(this.key);		// Attenzione: chiave e iv devono essere di lunghezza corretta !
				string iv = txt;
				int indx = iv.IndexOf(sep);
				if(indx >= 0)
					{
					iv = iv.Substring(indx);
					iv = iv.Replace(sep, "");
					txt = txt.Replace(sep + iv, "");
					aes.IV = Convert.FromBase64String(iv);			// size=256, L=32 caratteri. 44 caratteri in base64
					var decrypt = aes.CreateDecryptor();
					byte[] xBuff = null;
					using(var ms = new MemoryStream())
						{
						try
							{
							using(var cs = new CryptoStream(ms, decrypt, CryptoStreamMode.Write))
								{
								byte[] xXml = Convert.FromBase64String(txt);
								cs.Write(xXml, 0, xXml.Length);
								ok = true;
								}
							}
						catch(Exception ex)
							{
							AddErrorMessage(ex.Message);
							ok = false;
							}
						xBuff = ms.ToArray();
						}
					dec = Encoding.UTF8.GetString(xBuff);
					}
				}
			catch(Exception ex)
				{
				AddErrorMessage(ex.Message);
				ok = false;
				}
			return dec;
			}
#warning UNIFORMARE ENCRYPT/DECRYPT: return value bool, string by reference, parametro out o ref.
		protected bool ExecutePHPcmd(string command,string par1="",string par2="")		// Esecuzione comando dalla pagina PHP (risposta in this.tmp)
			{
			bool ok = false;
			var values = new NameValueCollection();
			values["a"] = command;
			values["b"] = par1;
			values["c"] = par2;
			try
				{
				Uri uri = new Uri(this.url);
				var response = this.client.UploadValues(uri, values);
				var responseString = Encoding.UTF8.GetString(response);
				this.phpmsg.rsp = responseString.ToString();
				ok = true;
				}
			catch(Exception ex)
				{			// Non serve chiamare Timer.Stop() perchè la connessione è comunque verificato dal timer
				AddErrorMessage(ex.Message);		
				}
			return ok;
#warning ATTENZIONE: eseguendo ExecutePHP senza fare il login, la risposta PHP è Utente disconnesso, ma non si genera errore.
#warning ATTENZIONE: eseguendo un secondo login, si crea una nuova connessione (che fallisce) senza chiudere la vecchia, che però viene perduta.
#warning ATTENZIONE: eseguire una risposta di convalida del login, possibilmente criptata.
            }
		protected bool ExecutePHPcmdAsync(string command,string par1="",string par2="")	// Esecuzione asincrona comando dalla pagina PHP
			{
			bool ok = false;
			var values = new NameValueCollection();
			values["a"] = command;
			values["b"] = par1;
			values["c"] = par2;
			try
				{
				Uri uri = new Uri(this.url);
				client.UploadValuesAsync(uri, values);								// Invia i valori in modo asincrono e continua
				ok = true;
				}
			catch(Exception ex)
				{
				AddErrorMessage(ex.Message);
				}
			return ok;
			}
		private void OnPHPcmdDone(Object sender,UploadValuesCompletedEventArgs e)	// Ricevere un messaggio e lo accoda, solo se senza errori.
			{										// L'handler può non essere statico, così da poter gestire più oggetti Connessione differenti.
			bool answer = false;
			PHPanswer tmp = new PHPanswer();
			try
				{
				var responseString = Encoding.UTF8.GetString(e.Result);				// Legge il messaggio di risposta...
				tmp.rsp = responseString.ToString();
				answer = true;														// Imposta il flag, c'è una risposta
				}
			catch(Exception ex)
				{
				tmp.errList.Add(ex.Message);										// Legge il messaggio di errore ma non aggiunge nulla alla coso
				}
			if(answer)																// Se c`è almeno una risposta...
				{
				if((tmp.errList.Count == 0) && (tmp.rsp.Length > 0) )
					{
					bool bok = false;
					PHPanswer dectmp = new PHPanswer();
					dectmp.rsp = DecryptMsg(tmp.rsp, ref bok);						// Decodifica il messaggio in dectmp
					if(bok == true)													// Se non vi sono errori...
						{
						this.phpmsgqueue.Enqueue(dectmp);							// ...la accoda
						}
					}
				
				}
			}
		protected bool EseguiRefresh()													// Esegue il refresh della connessione
			{
			bool ok = false;
			var values = new NameValueCollection();
			values["a"] = "refresh";
			values["b"] = "";
			values["c"] = "";
			string refrsp;
			try
				{
				Uri uri = new Uri(this.url);
				var response = this.client.UploadValues(uri, values);
				var responseString = Encoding.UTF8.GetString(response);
				refrsp = responseString.ToString();										// Risposta in una variabile locale
				if(refrsp.Length > 0)													// Se risposta non nulla = timeout, non connesso...
					{
					refreshTimer.Stop();												// Ferma il timer e aggiuge il messaggio di errore
					AddErrorMessage(string.Format("Errore durante il refresh:\n{0}", phpmsg.rsp));
					}
				}
			catch(Exception ex)
				{
				refreshTimer.Stop();
				AddErrorMessage(ex.Message);
				}
			return ok;
			}
		#endregion
		#region FUNZIONI PUBBLICHE
		public bool Login()											// Esegue login e attiva il refresh
			{
			ResetMessagesAndErrors();
			bool ok = ExecutePHPcmd("login", this.user, this.password);			// Esegue login
			if((ok == true) && (this.Errors == 0))									// Se login ok
				{
				refreshTimer.Start();											// Avvia il timer
				}
            else                                                                // Se problemi
                {                                                               // azzera il flag ed eswegue comunque un logout
                ok = false;
                ExecutePHPcmd("logout");
                }
            if(ok)
                {
                bool bok=false;
                string firstline ="";
                int indx;
                indx = this.phpmsg.rsp.IndexOf("\n");                           // Cerca il primo newline
                if (indx != -1)                                                 // Se lo trova, taglia la prima riga
                    {
                    firstline = this.phpmsg.rsp.Substring(0, indx);
                    this.phpmsg.rsp = this.phpmsg.rsp.Substring(indx+1);
                    this.phpmsg.errList.Add("Firstline: "+firstline);
                    }
                string dec_msg = DecryptMsg(this.phpmsg.rsp, ref bok);	           // Decodifica il messaggio del login (senza la prima riga)
                if (bok)
                    this.phpmsg.rsp = firstline + Environment.NewLine + dec_msg;      // Se  tutto ok, lo sostituisce nella risposta
                else
                    this.phpmsg.errList.Add("Fallita decrittazione risposta del login");
                }
			return ok;
			}
        public bool ClearLogged()
            {
            ResetMessagesAndErrors();
            bool ok = ExecutePHPcmd("clearlogged", this.user, this.password);         // Esegue login
            if ((ok == true) && (this.Errors == 0))                                 // Se login ok
                {
                refreshTimer.Start();                                           // Avvia il timer
                }
            else                                                                // Se problemi
                {                                                               // ezzera il flag ed eswegue comunque un logout
                ok = false;
                ExecutePHPcmd("logout");
                }
            return ok;

            }
        public bool Logout()										// Esegue logout e disattiva il refresh
			{
			ResetMessagesAndErrors();
			bool ok = ExecutePHPcmd("logout");
			refreshTimer.Stop();
			return ok;
			}
		public bool Status()										// Interroga la stato della connessione
			{
			ResetMessagesAndErrors();
			return ExecutePHPcmd("status");
			}
		public bool ProcessMessage(string cmdstr, bool crypt = true)	// Invia messaggio alla pagina php e ne legge la risposta
			{
			bool ok = false;
			ResetError();												// Cancella solo gli errori e la risposta, ma non...
			this.phpmsg.rsp = "";										// ...il messaggio da inviare (da impostare prima della chiamata)
			if(this.phpmsg.msg.Length > 0)								// Verifica che il messaggio non sia nullo
				{
				string enc_msg;
				if(crypt)
					enc_msg = EncryptMsg(this.phpmsg.msg);				// Codifica il messaggio, oppure in chiaro
				else
					enc_msg = this.phpmsg.msg;
				if(this.Errors == 0)									// Se nessun errore...
					{
					string par3 = "";
					if(crypt == false)
						par3 = "nocrypt";
					bool ok_exe = ExecutePHPcmd(cmdstr, enc_msg, par3);	// Lo invia alla pagina php e ne legge la risposta (in this.tmp)
					if((ok_exe == true) && (this.Errors == 0))			// Se non vi sono errori...
						{
						bool bok = false;
						string dec_msg;
						if(crypt)
							dec_msg = DecryptMsg(this.phpmsg.rsp, ref bok);	// Decodifica il messaggio
						else
							dec_msg = this.phpmsg.rsp;
						if(this.Errors == 0)							// Se non vi sono errori...
							{
							ok = true;									// Imposta il flag di risposta ok...
							this.phpmsg.rsp = dec_msg;					// ...e la risposta
							}
						}
					}
				}
			else
				{
				AddErrorMessage("Messaggio inviato nullo");
				}
			return ok;
			}
		public bool ProcessMessageAsync(string cmdstr)					// Invia messaggio (criptandolo) alla pagina php in modo asincrono
			{
			bool ok = false;
			ResetError();												// Cancella solo gli errori e la risposta, ma non...
			this.phpmsg.rsp = "";										// ...il messaggio da inviare (da impostare prima della chiamata)
			if(this.phpmsg.msg.Length > 0)								// Verifica che il messaggio non sia nullo
				{
				string enc_msg = EncryptMsg(this.phpmsg.msg);			// Codifica il messaggio
				if(this.Errors == 0)									// Se nessun errore...
					{
					bool ok_exe = ExecutePHPcmdAsync(cmdstr, enc_msg);	// Lo invia alla pagina php in modo asincrono
					if((ok_exe == true) && (this.Errors == 0))			// Se non vi sono errori...
						ok = true;										// ...imposta il flag di risposta ok
					}
				}
			else
				{
				AddErrorMessage("Messaggio inviato nullo");
				}
			return ok;
			}
		public string ConnectionMessages()							// Stringa con messaggio, risposta ed errori
			{
			return string.Format("Messaggio:\n{0}\n\nErrori:\n{1}\n\nRisposta:\n{2}", this.Message, this.ErrorMessages, this.Response);
			}
		public string QueueMessage()
			{
			string msg = "Coda vuota";
			if(this.phpmsgqueue.Count > 0)
				{
				this.phpmsg = this.phpmsgqueue.Dequeue();
				msg = ConnectionMessages();
				}
			return msg;
			}
		#endregion

#warning Aggiungere funzione di esecuzione comandi MySQL (leggere solo i parametri e/o tutto il comando MySQL completo) ProcessSQLmessage()
#warning Aggiungere funzioni per accodare comandi di una transazione (MySQL)
#warning Vedere come impilare i risultati con echo da php leggendoli da mysql (interrogazione e ciclo...).
#warning Leggere stringa da C#: valori separati da virgole o meglio '\n', va bene per gli ID.
#warning Per gli ID della query o per i campi ricavati da ID: vedere se '\n' è ok
#warning Attenzione a struttura albero e ordine (id precedente, id padre) e, a parte, id predecessore per pert. Oppure semplicemente id padre e n° di ordinamento (più semplice).
#warning Scrivere separatore di argomenti per inserimento/modifica record su richiesta in stringa da C#
		}
	}
