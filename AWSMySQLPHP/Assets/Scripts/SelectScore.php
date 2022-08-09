<?php

// POSTうけとり
$ranking_num = $_POST["rankingNum"];

try {
    $stmt = $pdo->query("SELECT * FROM `score` LIMIT '" . $ranking_num . "'");
    foreach ($stmt as $row) {
        $res = $res . $row['name'];
        $res = $res . $row['score'];
    }
} catch (PDOException $e) {
    var_dump($e->getMessage());
}

$pdo = null; // DB切断

echo $res; // クライアントに結果を返す
