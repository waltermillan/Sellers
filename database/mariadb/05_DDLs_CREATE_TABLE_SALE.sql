-- SellerDB.Sale definition

CREATE TABLE `Sale` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `id_seller` int(11) NOT NULL,
  `id_product` int(11) NOT NULL,
  `id_buyer` int(11) NOT NULL,
  `date` varchar(15) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_sales_product` (`id_product`),
  CONSTRAINT `fk_sales_product` FOREIGN KEY (`id_product`) REFERENCES `Product` (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;