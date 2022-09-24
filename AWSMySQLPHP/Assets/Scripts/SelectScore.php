<?php

// DB接続処理
require_once('DBConnect.php');
$pdo = connectDB();

// POSTうけとり
$ranking_num = $_POST["rankingNum"];
$order_by = $_POST["orderBy"];

try {
    // スコアの取得のSQLを実行
    $query = 'SELECT * FROM score_tbl ORDER BY score ';
    if ($order_by == 'OrderByAscending') {
        $query .= 'ASC ';
    } else {
        $query .= 'DESC ';
    }
    $query .= 'LIMIT :ranking_num;';
    $prepare = $pdo->prepare($query);
    $prepare->bindValue(':ranking_num', $ranking_num, PDO::PARAM_INT);
    $prepare->execute();

    // スコアを取得
    $stmt = $prepare->fetchAll(PDO::FETCH_ASSOC);
    $stms = array();

    // スコアをクライアント側に送る用の変数に格納
    foreach ($stmt as $row) {
        // echo $row->ToJson();
        $stms[] = array(
            'score_id' => $row['score_id'],
            'name' => $row['name'],
            'score' => $row['score']
        );
    }
    header('Content-Type: application/json; charset=UTF-8');
    echo json_encode($stms);
} catch (PDOException $e) {
    echo 'スコアの取得失敗' . "\n";
    var_dump($e->getMessage());
}
$pdo = null; // DB切断
