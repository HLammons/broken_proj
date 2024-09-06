<?php

$servername = "127.0.0.1";
$username = "root";
$password = "";
$dbname = "eyetracking";

//Variables submitted by user
$xt = $_POST["xt"];
$yt = $_POST["yt"];
$zt = $_POST["zt"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
        //Insert the user and password into the database
        $sql = "INSERT INTO dataprobe1 (xt, yt, zt) VALUES ('".$xt."', '".$yt."', '".$zt."')";

        if ($conn->query($sql) === TRUE) {
            //echo "New eye-tracking data added!";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }


$conn->close();

?>
