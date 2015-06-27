<?php
// Variabili globali
$timeout = 60;											// timeout in secondi
$pausaWrong = 1;										// Pausa se utente errato
$sep = "*";												// Separatore
$keystrval   = "11111111111111111111111111111111";		// Password (fittizia) 32 caratteri 

$host="127.0.0.1";										// Server name
$username="root";										// Mysql username DA CAMBIARE
$password="";											// Mysql password DA AGGIORNARE
$db_name="my_fred68";									// Database name
$tbl_name="members";									// Table name
$usrColumn="usrname";									// Nomi delle colonne
$pwdColumn="passwd";		
$keystr="keystr";										// Nome colonna con chiave di criptatura

$pertdb = "Pert";										// Nome database
$pwddb="pwddb";											// Password

function serverPost($t1, $t2, $sec)						// FUNZIONE DI PROVA
	{
	sleep($sec);
	$txt = $t1."\n".$t2."\n".microtime();
	return $txt;
	}
function addpadding($string, $blocksize = 32)			// Aggiunge padding PKCS7
	{													// Thanks to http://blog.djekldevelopments.co.uk/?p=334)
    $pad = $blocksize - (strlen($string) % $blocksize);
    $string .= str_repeat(chr($pad), $pad);
    return $string;
	}
function strippadding($string)							// Elimina padding
	{													// Thanks to http://blog.djekldevelopments.co.uk/?p=334)
	$slast = ord(substr($string, -1));
	$slastc = chr($slast);
	$pcheck = substr($string, -$slast);
	if(preg_match("/$slastc{".$slast."}/", $string))
		{
		$string = substr($string, 0, strlen($string)-$slast);
		return $string;
		}
	else
		{
		return false;
		}
	}
function encrypt($string="")							// Criptatura messaggio
	{
	global $sep;
	$msg = "";
	if(isset($_SESSION['keystr']))
		{
		$keystrval = $_SESSION['keystr'];
		$enc = "";											// Messaggio criptato (inizializzato a zero)
		$size = mcrypt_get_iv_size(MCRYPT_RIJNDAEL_256, MCRYPT_MODE_CBC);	// Dimensione dell'iv
		$ivstrauto = mcrypt_create_iv ($size);				// Creazione di un iv casuale [in quale formato ?]
		$key = base64_decode($keystrval);
		$enc = mcrypt_encrypt(MCRYPT_RIJNDAEL_256, $key, addpadding($string), MCRYPT_MODE_CBC, $ivstrauto);	// Codifica
		$msg = base64_encode($enc).$sep.base64_encode($ivstrauto);				// Unisce messaggio criptato, sepratore e iv
		}
    return $msg;
	}
function decrypt($msg = "")								// Decriptatura messaggio
	{
	global $sep;
	$ret = "";
	if(isset($_SESSION['keystr']))
		{
		$keystrval = $_SESSION['keystr'];
		$key = base64_decode($keystrval);
		$iv = substr($msg, strrpos($msg, $sep) + 1);		// Estrae l'iv
		$msg = str_replace($sep.$iv, "", $msg);				// Elimina iv e separatore
		$string = base64_decode($msg);
		$ret = strippadding(mcrypt_decrypt(MCRYPT_RIJNDAEL_256, $key, $string, MCRYPT_MODE_CBC, base64_decode($iv)));
		}
	return $ret;
	}
function checklogin($usr,$pwd)							// Connette l'utente
	{
	global $username, $password, $db_name, $host, $usrColumn, $pwdColumn, $tbl_name, $keystr, $keystrval, $pwddb;
	$msg = "-";
	try
		{												// Apre connessione a database (mysql o altro)
		$conn = new PDO("mysql:host=$host;dbname=$db_name", $username, $password);
		$conn->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
		}
	catch(PDOException $e)
		 {
		 $msg = $e->getMessage();
		 }
	$myusername=preg_replace("/[^a-zA-Z0-9@.]+/", "", $usr);			// Filtra...
	$mypassword=sha1(preg_replace("/[^a-zA-Z0-9@.]+/", "", $pwd));		// ...solo alfanumerico, punto e chiocciola
	$count=0;
	$sql="SELECT COUNT(*) FROM $tbl_name WHERE ".$usrColumn."='$myusername' AND ".$pwdColumn."='$mypassword'";
	try
		{
		$stmt = $conn->prepare($sql);
		$stmt->execute();
		$count=$stmt->fetchColumn();
		}
	catch(PDOException $e)
		{
		$msg = "Error: ".$e->getMessage();
		}
	$sql="SELECT ".$keystr.", ".$pwddb." FROM $tbl_name WHERE ".$usrColumn."='$myusername' AND ".$pwdColumn."='$mypassword'";
	try
		{
		$stmt = $conn->prepare($sql);
		$stmt->execute();
		$rows = $stmt->fetchAll();				// Estrae tutte le righe della query
		$row = $rows[0];						// Sceglie la prima (ed unica)
		$keystrval = $row[0];					// Legge la chiave
		$pwddbval = $row[1];					// Legge la password del db
		}
	catch(PDOException $e)
		{
		$msg = "Error: ".$e->getMessage();
		}
	$conn = null;								// Disconnette dal database
	if($count==1)								// Login corretto
		{
		if(!isset($_SESSION['usr']))			// Controllare prima se $_SESSION['usr'] esiste e se è uguale a $myusername
			{
			$_SESSION['usr'] = $myusername;
			$_SESSION['loginTime'] = time();
			$_SESSION['lastTime'] = time();
			$_SESSION['keystr'] = $keystrval;
			$_SESSION['pwddb'] = $pwddbval;
			$msg = "Login completato: ".$_SESSION['usr'];
			}
		else
			{
			$msg = "Login già eseguito";
			}
		}
	else
		{
		$msg = "Utente o password errati";
		session_unset();
		session_destroy();
		}
	return $msg;
	}
function logout()										// Disconnette l'utente
	{
	session_unset();
	session_destroy();
	return "Utente disconnesso.";
	}
function refreshStat()									// Stato della connessione
	{
	global $timeout;
	$msg = "";
	if (isset($_SESSION['usr']))
		{
		$tt = time()-$_SESSION['lastTime'];
		$_SESSION['lastTime'] = time();
		if($tt > $timeout)
			{
			$msg = "Trascorsi ".$tt." secondi. Timeout dopo ".$timeout."s.";
			session_unset();
			session_destroy();
			}
		else
			{
			$msg = "Utente: ".$_SESSION['usr']."\n";
			$msg .= "Tempo trascorso dal login: ".(time()-$_SESSION['loginTime'])."\n";
			$msg .= "Tempo trascorso da ultima operazione: ".$tt;
			}
		}
	else
		{
		$msg = "Utente non connesso";
		session_unset();
		session_destroy();
		}
	return $msg;
	}
function refresh()										// Refresh. Messaggio solo se errori
	{
	global $timeout;
	$msg = "";
	if (isset($_SESSION['usr']))
		{
		$tt = time()-$_SESSION['lastTime'];
		$_SESSION['lastTime'] = time();
		if($tt > $timeout)
			{
			session_unset();
			session_destroy();
			$msg = "Tempo scaduto";
			}
		}
	else
		{
		session_unset();
		session_destroy();
		$msg = "Utente non connesso";
		}
	return $msg;
	}
function execute($cmd = "")								// Esecuzione di un comando
	{
	$msg = "Messaggio\n".decrypt($cmd)."\nricevuto...";
	echo encrypt($msg);
	}
function query($cmd = "", $crypt = true)				// Esecuzione di una query mysql
	{
	global $pertdb, $host;
	if($crypt)
		$sql = decrypt($cmd);							// Legge il comando
	else
		$sql = $cmd;
	$c = null;											// Connessione
	if ( isset($_SESSION['usr']) && isset($_SESSION['pwddb']))
	 	{
	 	try
	 		{
	 		$c = new PDO("mysql:host=$host;dbname=$pertdb", $_SESSION['usr'], $_SESSION['pwddb']);
	 		$c->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
			try
				{
				$stmt = $c->prepare($sql);
				$stmt->execute();
				$result = $stmt->fetchAll(PDO::FETCH_ASSOC);
				if($result !== false)
					{
					$msg = "Query eseguita.\n";  
					foreach($result as $r)
						{
						foreach($r as $x)
							$msg .= $x."\t";
						$msg .= "\n";
						}
					}
				else
					{
					$msg = "Nessun risultato";
					}
				}
			catch(PDOException $e)
				{
				$msg = "Query error: ".$e->getMessage();
				}
	 		}
	 	catch(PDOException $e)
	 		{
			$msg = "Connection error:\n";
	 		$msg .= $e->getMessage();
	 		}
	 	$c = null;										// Disconnette dal database
		}
	if($crypt)
		$msg = encrypt($msg);
	return $msg;
	}
function sqlcom($cmd = "", $crypt = true)				// Esecuzione di un comando mysql
	{
	global $pertdb, $host;
	$msg = "";
	if($crypt)
		$sql = decrypt($cmd);							// Legge il comando
	else
		$sql = $cmd;
	$c = null;											// Connessione
	if ( isset($_SESSION['usr']) && isset($_SESSION['pwddb']))
	 	{
	 	try
	 		{
	 		$c = new PDO("mysql:host=$host;dbname=$pertdb", $_SESSION['usr'], $_SESSION['pwddb']);
	 		$c->setAttribute(PDO::ATTR_ERRMODE, PDO::ERRMODE_EXCEPTION);
			try
				{
				$count = $c->exec($sql);
				if($count !== false)
					$msg = "Affected ".$count." rows.";
				else
					$msg = "No rows affected";
				}
			catch(PDOException $e)
				{
				$msg .= "Exec error: ".$e->getMessage();
				}
	 		}
	 	catch(PDOException $e)
	 		{
			$msg = "Connection error:\n";
	 		$msg .= $e->getMessage();
	 		}
	 	$c = null;										// Disconnette dal database
		}
	if($crypt)
		$msg = encrypt($msg);
	return $msg;
	}
function command()										// Lettura ed esecuzione di un comando (Post)
	{
	$a = $_POST["a"];
	$b = $_POST["b"];
	$c = $_POST["c"];
	switch($a)
		{
		case "login":
			echo checklogin($b,$c);
			break;
		case "logout":
			echo logout();
			break;
		case "status":
			echo refreshStat();
			break;
		case "encode":
			echo encrypt($b);
			refresh();
			break;
		case "decode":
			echo decrypt($b);
			refresh();
			break;
		case "exe":
			echo execute($b);
			refresh();
			break;
		case "query":
			if($c == "nocrypt")
				echo query($b,false);
			else
				echo query($b);
			refresh();
			break;
		case "exec":
			if($c == "nocrypt")
				echo sqlcom($b,false);
			else
				echo sqlcom($b);
			refresh();
			break;
		case "refresh":
			echo refresh();		// echo refresh solo qui, per verificare connessione
			break;
		default:
			echo "command unknown";
			break;
		}
	}
	?>
