<?php

// DB接続処理
require_once('DBConnect.php');
$pdo = connectDB();

// POSTうけとり
$name = $_POST["name"];
$score = $_POST["score"];

try {
    // スコアの登録のSQLを実行
    $query = 'INSERT INTO score_tbl (name, score) VALUES (:name, :score);';
    $prepare = $pdo->prepare($query);
    $prepare->bindValue(':name', $name, PDO::PARAM_STR);
    $prepare->bindValue(':score', $score);
    $prepare->execute();
} catch (PDOException $e) {
    echo '';
    var_dump($e->getMessage());
}

$pdo = null; // DB切断
