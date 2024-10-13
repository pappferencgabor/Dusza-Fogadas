-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- ホスト: 127.0.0.1
-- 生成日時: 2024-10-13 22:40:25
-- サーバのバージョン： 10.4.32-MariaDB
-- PHP のバージョン: 8.0.30

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- データベース: `betting`
--

-- --------------------------------------------------------

--
-- テーブルの構造 `alanyok`
--

CREATE TABLE `alanyok` (
  `id` int(11) NOT NULL,
  `nev` varchar(128) DEFAULT NULL,
  `jatekId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- テーブルのデータのダンプ `alanyok`
--

INSERT INTO `alanyok` (`id`, `nev`, `jatekId`) VALUES
(1, 'Janos', 1),
(2, 'Aladar', 1),
(3, 'Pingvin', 1);

-- --------------------------------------------------------

--
-- テーブルの構造 `eredmenyek`
--

CREATE TABLE `eredmenyek` (
  `id` int(11) NOT NULL,
  `jatekId` int(11) DEFAULT NULL,
  `alanyId` int(11) DEFAULT NULL,
  `esemenyId` int(11) DEFAULT NULL,
  `esemenyErteke` varchar(128) DEFAULT NULL,
  `szorzo` float DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

-- --------------------------------------------------------

--
-- テーブルの構造 `esemenyek`
--

CREATE TABLE `esemenyek` (
  `id` int(11) NOT NULL,
  `nev` varchar(128) DEFAULT NULL,
  `jatekId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- テーブルのデータのダンプ `esemenyek`
--

INSERT INTO `esemenyek` (`id`, `nev`, `jatekId`) VALUES
(1, 'Lefutasi ido', 1),
(2, 'Hibara kifutas', 1),
(3, 'Bugmentes lefutas', 1);

-- --------------------------------------------------------

--
-- テーブルの構造 `felhasznalok`
--

CREATE TABLE `felhasznalok` (
  `id` int(11) NOT NULL,
  `nev` varchar(128) DEFAULT NULL,
  `jelszo` varchar(255) DEFAULT NULL,
  `pontok` int(11) DEFAULT NULL,
  `szerepkor` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- テーブルのデータのダンプ `felhasznalok`
--

INSERT INTO `felhasznalok` (`id`, `nev`, `jelszo`, `pontok`, `szerepkor`) VALUES
(1, 'sketch', 'MnoSenXFx0SnvUrwVBVEJ1TeNvLXapI4wk6MDvey/PvLhQR5DDI6xI1utfGPgrkt', 100, 'A'),
(2, 'tesztSima', 'sHzkjJS5ohkAfvIv+JOkBUJV/Cfj3YOVh6Mhdym6D4VHzB8DbwVXUeFqXSLpRZrL', 0, 'F'),
(3, 'tesztAdmin', 'V8QkUxM/ZiP2kMW8CWWw+is9HqvuXTz+Npd0cUzThSn9lMDvOcBH8MFf3UoYU7vO', 0, 'A');

-- --------------------------------------------------------

--
-- テーブルの構造 `fogadasok`
--

CREATE TABLE `fogadasok` (
  `id` int(11) NOT NULL,
  `felhasznaloid` int(11) DEFAULT NULL,
  `jatekId` int(11) DEFAULT NULL,
  `alanyId` int(11) DEFAULT NULL,
  `esemenyId` int(11) DEFAULT NULL,
  `tet` varchar(255) DEFAULT NULL,
  `fogadasErteke` varchar(128) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- テーブルのデータのダンプ `fogadasok`
--

INSERT INTO `fogadasok` (`id`, `felhasznaloid`, `jatekId`, `alanyId`, `esemenyId`, `tet`, `fogadasErteke`) VALUES
(1, 1, 1, 1, 1, '10', '100');

-- --------------------------------------------------------

--
-- テーブルの構造 `jatekok`
--

CREATE TABLE `jatekok` (
  `id` int(11) NOT NULL,
  `szervezoNev` varchar(128) DEFAULT NULL,
  `nev` varchar(255) DEFAULT NULL,
  `alanyokSzama` int(11) DEFAULT NULL,
  `status` varchar(255) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8 COLLATE=utf8_hungarian_ci;

--
-- テーブルのデータのダンプ `jatekok`
--

INSERT INTO `jatekok` (`id`, `szervezoNev`, `nev`, `alanyokSzama`, `status`) VALUES
(1, 'Horvath Janos', 'ProgKing2022', 3, 'Aktiv');

--
-- ダンプしたテーブルのインデックス
--

--
-- テーブルのインデックス `alanyok`
--
ALTER TABLE `alanyok`
  ADD PRIMARY KEY (`id`),
  ADD KEY `jatekId` (`jatekId`);

--
-- テーブルのインデックス `eredmenyek`
--
ALTER TABLE `eredmenyek`
  ADD PRIMARY KEY (`id`),
  ADD KEY `jatekId` (`jatekId`),
  ADD KEY `alanyId` (`alanyId`),
  ADD KEY `esemenyId` (`esemenyId`);

--
-- テーブルのインデックス `esemenyek`
--
ALTER TABLE `esemenyek`
  ADD PRIMARY KEY (`id`);

--
-- テーブルのインデックス `felhasznalok`
--
ALTER TABLE `felhasznalok`
  ADD PRIMARY KEY (`id`);

--
-- テーブルのインデックス `fogadasok`
--
ALTER TABLE `fogadasok`
  ADD PRIMARY KEY (`id`),
  ADD KEY `felhasznaloid` (`felhasznaloid`),
  ADD KEY `jatekId` (`jatekId`),
  ADD KEY `alanyId` (`alanyId`),
  ADD KEY `esemenyId` (`esemenyId`);

--
-- テーブルのインデックス `jatekok`
--
ALTER TABLE `jatekok`
  ADD PRIMARY KEY (`id`);

--
-- ダンプしたテーブルの AUTO_INCREMENT
--

--
-- テーブルの AUTO_INCREMENT `alanyok`
--
ALTER TABLE `alanyok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- テーブルの AUTO_INCREMENT `eredmenyek`
--
ALTER TABLE `eredmenyek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- テーブルの AUTO_INCREMENT `esemenyek`
--
ALTER TABLE `esemenyek`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- テーブルの AUTO_INCREMENT `felhasznalok`
--
ALTER TABLE `felhasznalok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- テーブルの AUTO_INCREMENT `fogadasok`
--
ALTER TABLE `fogadasok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- テーブルの AUTO_INCREMENT `jatekok`
--
ALTER TABLE `jatekok`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- ダンプしたテーブルの制約
--

--
-- テーブルの制約 `alanyok`
--
ALTER TABLE `alanyok`
  ADD CONSTRAINT `alanyok_ibfk_1` FOREIGN KEY (`jatekId`) REFERENCES `jatekok` (`id`);

--
-- テーブルの制約 `eredmenyek`
--
ALTER TABLE `eredmenyek`
  ADD CONSTRAINT `eredmenyek_ibfk_1` FOREIGN KEY (`jatekId`) REFERENCES `jatekok` (`id`),
  ADD CONSTRAINT `eredmenyek_ibfk_2` FOREIGN KEY (`alanyId`) REFERENCES `alanyok` (`id`),
  ADD CONSTRAINT `eredmenyek_ibfk_3` FOREIGN KEY (`esemenyId`) REFERENCES `esemenyek` (`id`);

--
-- テーブルの制約 `fogadasok`
--
ALTER TABLE `fogadasok`
  ADD CONSTRAINT `fogadasok_ibfk_1` FOREIGN KEY (`felhasznaloid`) REFERENCES `felhasznalok` (`id`),
  ADD CONSTRAINT `fogadasok_ibfk_2` FOREIGN KEY (`jatekId`) REFERENCES `jatekok` (`id`),
  ADD CONSTRAINT `fogadasok_ibfk_3` FOREIGN KEY (`alanyId`) REFERENCES `alanyok` (`id`),
  ADD CONSTRAINT `fogadasok_ibfk_4` FOREIGN KEY (`esemenyId`) REFERENCES `esemenyek` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
