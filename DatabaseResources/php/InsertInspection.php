<?php

$servername = "127.0.0.1";
$username = "root";
$password = "";
$dbname = "eyetracking";
$tablename = "inspection";

//Variables submitted by user
$name = $_POST["name"];
$date = $_POST["date"];
$data = $_POST["data"];
$inspector_id = $_POST["inspector_id"];
$reference_inspection_id = $_POST["reference_inspection_id"];
$score = $_POST["score"];

// Create connection
$conn = new mysqli($servername, $username, $password, $dbname);

// Check connection
if ($conn->connect_error) {
  die("Connection failed: " . $conn->connect_error);
}
        //Insert the user and password into the database
        $sql = "INSERT INTO $tablename (name, data, inspector_id, reference_inspection_id, score) VALUES ('".$name."','$data','".$inspector_id."','".$reference_inspection_id."','".$score."')";

if ($conn->query($sql) === TRUE) {
            echo "New $tablename added!";
 } else {
            echo "Error: " . $sql . "<br>" . $conn->error;
}
$conn->close();

?>