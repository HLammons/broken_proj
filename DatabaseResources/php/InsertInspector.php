<?php

$servername = "127.0.0.1";
$username = "root";
$password = "";
$dbname = "eyetracking";
$tablename = "inspector";

//Variables submitted by user
$first_name = $_POST["first_name"];
$last_name = $_POST["last_name"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
        //Insert the user and password into the database
        $sql = "INSERT INTO $tablename (first_name, last_name) VALUES ('".$first_name."','".$last_name."')";

        if ($conn->query($sql) === TRUE) {
            echo "New $tablename added!";
        } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
        }


$conn->close();

?>