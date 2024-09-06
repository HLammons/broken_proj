<?php
include('connection.php');
$tablename = "inspection";

if (isset($_GET['id']) && $_GET['id'] != "") {

  $id = $_GET["id"];
  $sql = "SELECT * FROM $tablename WHERE id=$id";
  $result = mysqli_query($con, $sql);
  $row = mysqli_fetch_array($result, MYSQLI_ASSOC);

  $response["Status"] = "true";
  $response['Id'] = $row['id'];
  $response['Date'] = $row['date'];
  $response['Data'] = $row['data'];
  $response['Inspector_id'] = $row['inspector_id'];
  $response['Reference_inspection_id'] = $row['reference_inspection_id'];
  $response['Score'] = $row['score'];
  
  
  //$response["inspections"] = $inspectorData;

} else {
  $response["status"] = "false";
  $response["message"] = "No inspection found!";
}
echo json_encode($response); exit;
?>