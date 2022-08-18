<?php

// DB接続処理
require_once('DBConnect.php');
$pdo = connectDB();

// POSTうけとり
$name = $_POST["name"];
$score = $_POST["score"];

try {
    // スコアの登録のSQLを実行
    $query = 'INSERT INTO score_tbl VALUES (:name , :score);';
    $prepare = $pdo->prepare($query);
    $prepare->bind_value(':name', $name, PDO::PARAM_STR);
    $prepare->bind_value(':score', $score);
    $pdo->execute();
} catch (PDOException $e) {
    var_dump($e->getMessage());
}

$pdo = null; // DB切断
