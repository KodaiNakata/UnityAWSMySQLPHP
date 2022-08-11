<?php

// DB接続処理
require_once('DBConnect.php');
$pdo = connectDB();

// POSTうけとり
$name = $_POST["name"];
$score = $_POST["score"];

try {
    $stmt = $pdo->query("INSERT INTO `score` VALUES ('" . $name . "','" . $score . "'");
    foreach ($stmt as $row) {
        $res = $res . $row['name'];
        $res = $res . $row['score'];
    }
} catch (PDOException $e) {
    var_dump($e->getMessage());
}

$pdo = null; // DB切断

echo $res; // クライアントに結果を返す
