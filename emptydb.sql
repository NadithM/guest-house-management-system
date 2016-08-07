-- phpMyAdmin SQL Dump
-- version 4.5.1
-- http://www.phpmyadmin.net
--
-- Host: 127.0.0.1
-- Generation Time: Jul 19, 2016 at 12:29 AM
-- Server version: 10.1.13-MariaDB
-- PHP Version: 5.5.34

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `test`
--

-- --------------------------------------------------------

--
-- Table structure for table `itemlist`
--

CREATE TABLE `itemlist` (
  `item_id` int(4) NOT NULL,
  `item` varchar(100) NOT NULL,
  `Location` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- --------------------------------------------------------

--
-- Table structure for table `roomd`
--

CREATE TABLE `roomd` (
  `id` int(11) NOT NULL,
  `Location` varchar(100) NOT NULL,
  `TotalRooms` int(3) NOT NULL,
  `full` int(3) NOT NULL,
  `empty` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roomd`
--

INSERT INTO `roomd` (`id`, `Location`, `TotalRooms`, `full`, `empty`) VALUES
(1, 'Colombo', 5, 0, 5),
(2, 'Trincomalee', 6, 0, 6);

-- --------------------------------------------------------

--
-- Table structure for table `roomitem`
--

CREATE TABLE `roomitem` (
  `Room_No` int(11) NOT NULL,
  `Room_Free` varchar(3) NOT NULL DEFAULT 'Yes',
  `Room_Full` varchar(3) NOT NULL DEFAULT 'No',
  `Location` varchar(50) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Dumping data for table `roomitem`
--

INSERT INTO `roomitem` (`Room_No`, `Room_Free`, `Room_Full`, `Location`) VALUES
(1, 'Yes', 'No', 'Colombo'),
(2, 'Yes', 'No', 'Colombo'),
(3, 'Yes', 'No', 'Colombo'),
(4, 'Yes', 'No', 'Colombo'),
(5, 'Yes', 'No', 'Colombo'),
(2, 'Yes', 'No', 'Trincomalee'),
(1, 'Yes', 'No', 'Trincomalee'),
(4, 'Yes', 'No', 'Trincomalee'),
(5, 'Yes', 'No', 'Trincomalee'),
(6, 'Yes', 'No', 'Trincomalee'),
(3, 'Yes', 'No', 'Trincomalee');

-- --------------------------------------------------------

--
-- Table structure for table `storage`
--

CREATE TABLE `storage` (
  `Location` varchar(100) NOT NULL,
  `Item` varchar(50) NOT NULL,
  `No_of_items` int(3) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

--
-- Indexes for dumped tables
--

--
-- Indexes for table `itemlist`
--
ALTER TABLE `itemlist`
  ADD PRIMARY KEY (`item_id`,`Location`);

--
-- Indexes for table `roomd`
--
ALTER TABLE `roomd`
  ADD PRIMARY KEY (`id`);

--
-- Indexes for table `storage`
--
ALTER TABLE `storage`
  ADD PRIMARY KEY (`Location`,`Item`);

--
-- AUTO_INCREMENT for dumped tables
--

--
-- AUTO_INCREMENT for table `itemlist`
--
ALTER TABLE `itemlist`
  MODIFY `item_id` int(4) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;
--
-- AUTO_INCREMENT for table `roomd`
--
ALTER TABLE `roomd`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
