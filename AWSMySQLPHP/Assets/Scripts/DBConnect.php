<?php

/**
 * DB接続処理
 *
 * @return object DB接続結果
 */
function connectDB()
{
    $dsn = 'mysql:dbname=unity_test;host=localhost;charset=utf8'; // DBのアドレス
    $username = ''; // ユーザ名
    $password = ''; // パスワード

    try {
        $pdo = new PDO($dsn, $username, $password);
    } catch (PDOException $e) {
        echo 'DB接続失敗'."\n";
        exit('' . $e->getMessage());
    }

    return $pdo;
}
?>