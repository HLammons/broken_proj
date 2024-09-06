<?php
include('connection.php');
$tablename = "inspector";

if (isset($_GET['id']) && $_GET['id'] != "") {

  $id = $_GET["id"];
  $sql = "SELECT * FROM $tablename WHERE id=$id";
  $result = mysqli_query($con, $sql);
  $row = mysqli_fetch_array($result, MYSQLI_ASSOC);

  $inspectorData['id'] = $row['id'];
  $inspectorData['first_name'] = $row['first_name'];
  $inspectorData['last_name'] = $row['last_name'];
  
  $response["status"] = "true";
  $response["inspectors"] = $inspectorData;

} else {
  $response["status"] = "false";
  $response["message"] = "No customer(s) found!";
}
echo json_encode($response); exit;
?>