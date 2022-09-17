<?php

// DB接続処理
require_once('DBConnect.php');
$pdo = connectDB();

// POSTうけとり
$ranking_num = $_POST["rankingNum"];
$order_by = $_POST["orderBy"];

try {
    // スコアの取得のSQLを実行
    $query = 'SELECT * FROM score_tbl ';
    if ($order_by == 'OrderByAscending') {
        $query .= 'ORDER BY ASC ';
    } else {
        $query .= 'ORDER BY DESC ';
    }
    $query .= 'LIMIT :ranking_num;';
    $prepare = $pdo->prepare($query);
    $prepare->bind_value(':ranking_num', $ranking_num, PDO::PARAM_INT);
    $stmt = $pdo->execute();

    // クライアント側に送るスコアのデータ
    foreach ($stmt as $row) {
        $res = $res . $row['name'];
        $res = $res . $row['score'];
    }
} catch (PDOException $e) {
    echo 'スコアの取得失敗';
    var_dump($e->getMessage());
}

$pdo = null; // DB切断

echo $res; // クライアントに結果を返す
?>