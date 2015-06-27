<?php
session_start();
include 'php/f.php';
$msg = $_POST["msg"];
echo decrypt($msg);
?>
