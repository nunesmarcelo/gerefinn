-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: financeiro
-- ------------------------------------------------------
-- Server version	5.5.5-10.1.33-MariaDB

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `categoria`
--

DROP TABLE IF EXISTS `categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categoria` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `tipo` enum('AC','ANC','PC','PNC','PL') NOT NULL,
  `descricao` varchar(1000) DEFAULT NULL,
  `rd` enum('R','D') NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categoria`
--

LOCK TABLES `categoria` WRITE;
/*!40000 ALTER TABLE `categoria` DISABLE KEYS */;
INSERT INTO `categoria` VALUES (1,'SERVIÇO MANUTENÇÃO','','AC',NULL,'R'),(2,'MANUTENÇÃO VEÍCULO','','PC',NULL,'D'),(3,'MATERIAIS PARA MANUTENÇÃO','','PC','filtros, bobinas, correias, parafusos, etc.','D'),(4,'PROJETOs','','AC','guindaste','R'),(12,'PAGAMENTOS','','AC',NULL,'R'),(13,'INVESTIMENTOS','','ANC',NULL,'R'),(14,'DEPRECIAÇÃO','','PNC',NULL,'D'),(15,'ENTRADA DE CAPITAL','','PL',NULL,'D'),(16,'IMPOSTO - ICMS','','PC',NULL,'D');
/*!40000 ALTER TABLE `categoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contasaldo`
--

DROP TABLE IF EXISTS `contasaldo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contasaldo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `banco` varchar(45) NOT NULL,
  `saldo` decimal(12,2) NOT NULL DEFAULT '0.00',
  `status` bit(1) NOT NULL DEFAULT b'1',
  `agencia` varchar(45) DEFAULT NULL,
  `conta` varchar(45) DEFAULT NULL,
  `titular` varchar(45) DEFAULT NULL,
  `tipo` enum('CORRENTE','POUPANCA','MISTA') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contasaldo`
--

LOCK TABLES `contasaldo` WRITE;
/*!40000 ALTER TABLE `contasaldo` DISABLE KEYS */;
INSERT INTO `contasaldo` VALUES (2,'CAIXA MATRIZ','CAIXA',-140.78,'','23221','hugo',NULL,'MISTA'),(3,'HSTC','HSTC',200.00,'',NULL,NULL,NULL,'POUPANCA'),(4,'ITAÚ','ITAÚ',2033.00,'',NULL,NULL,NULL,'CORRENTE'),(5,'AGENCIA 3440','BRASIL',250.00,'',NULL,NULL,NULL,'MISTA');
/*!40000 ALTER TABLE `contasaldo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estoque`
--

DROP TABLE IF EXISTS `estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `estoque` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `responsavel` varchar(45) DEFAULT NULL,
  `telefone` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `rua` varchar(45) DEFAULT NULL,
  `numero` int(6) DEFAULT NULL,
  `bairro` varchar(45) DEFAULT NULL,
  `cidade` varchar(45) DEFAULT NULL,
  `estado` varchar(45) DEFAULT NULL,
  `cep` varchar(45) DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=8 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoque`
--

LOCK TABLES `estoque` WRITE;
/*!40000 ALTER TABLE `estoque` DISABLE KEYS */;
INSERT INTO `estoque` VALUES (3,'MATRIZ','Joaquim','(31) 3775-5959','matriz@teste.com.br',NULL,NULL,NULL,NULL,NULL,NULL,''),(4,'QUARTINHO FUNDOS','Fernando','(31) 996262966',NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),(6,'CASA TIA LU','LU',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,''),(7,'PAPELARIA ESTUDAR','Maria',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'');
/*!40000 ALTER TABLE `estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `instituicao`
--

DROP TABLE IF EXISTS `instituicao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `instituicao` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `cnpj` varchar(45) DEFAULT NULL,
  `email` varchar(45) DEFAULT NULL,
  `telefone1` varchar(45) DEFAULT NULL,
  `telefone2` varchar(45) DEFAULT NULL,
  `responsavel` varchar(45) DEFAULT NULL,
  `rua` varchar(45) DEFAULT NULL,
  `numero` int(11) DEFAULT NULL,
  `bairro` varchar(45) DEFAULT NULL,
  `cidade` varchar(45) DEFAULT NULL,
  `estado` varchar(45) DEFAULT NULL,
  `cep` varchar(45) DEFAULT NULL,
  `fc` enum('F','C') NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `instituicao`
--

LOCK TABLES `instituicao` WRITE;
/*!40000 ALTER TABLE `instituicao` DISABLE KEYS */;
INSERT INTO `instituicao` VALUES (1,'CASA DE MATERIAIS LUCENA',NULL,'lucena@gmail.com',NULL,NULL,'TOBIAS',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(2,'MECÂNICA HILTON','11.111.111/1111-11','hilton@teste.com.br','(31) 3776-5985','(11) 2525-6255','HILTON',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(4,'VALE','22.222.222/2222-22','vale@teste.com.br','(21) 2222-5550',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'C'),(10,'CASAS MOURA','11.111.111/1222-22','moura@gmail.com','(31) 33333-3332','(31) 99659-8350','Joaquim Mourão','Avenida Doutor Renato Azeredo',221,'Canaã','Sete Lagoas','MG','35.700-312','C'),(11,'CASA DE PEÇAS UNIÃO','25.211.099/2000-00','uniaopecas@gmail.com','(31) 3372-3382','(31) 3322-1212','MEIRAo','Rua João Mendes',222,'Piedade','Sete Lagoas','MG','35.700-226','F'),(12,'TANURE',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'C');
/*!40000 ALTER TABLE `instituicao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lancamento`
--

DROP TABLE IF EXISTS `lancamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lancamento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `descricao` varchar(45) NOT NULL,
  `quantidade` int(11) DEFAULT NULL,
  `valor` decimal(12,2) NOT NULL,
  `pago` bit(1) NOT NULL DEFAULT b'0',
  `numerotitulo` varchar(45) DEFAULT NULL,
  `datavencimento` datetime DEFAULT NULL,
  `dataemissao` datetime DEFAULT NULL,
  `datacadastro` datetime NOT NULL,
  `observacao` varchar(200) DEFAULT NULL,
  `categoria_id` int(11) NOT NULL,
  `instituicao_id` int(11) NOT NULL,
  `contasaldo_id` int(11) NOT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_lancamento_categoria1_idx` (`categoria_id`),
  KEY `fk_lancamento_instituicao1_idx` (`instituicao_id`),
  KEY `fk_lancamento_contasaldo1_idx` (`contasaldo_id`),
  KEY `fk_lancamento_usuario1_idx` (`usuario_id`),
  CONSTRAINT `fk_lancamento_categoria1` FOREIGN KEY (`categoria_id`) REFERENCES `categoria` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_contasaldo1` FOREIGN KEY (`contasaldo_id`) REFERENCES `contasaldo` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_instituicao1` FOREIGN KEY (`instituicao_id`) REFERENCES `instituicao` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lancamento`
--

LOCK TABLES `lancamento` WRITE;
/*!40000 ALTER TABLE `lancamento` DISABLE KEYS */;
INSERT INTO `lancamento` VALUES (1,'1H DE SERVIÇO - SOLDA',NULL,120.00,'',NULL,'2018-10-15 00:00:00','2018-10-07 00:00:00','2018-10-07 14:04:08',NULL,1,1,2,NULL),(3,'SERVIÇO PARA EMPRESAS DURAÇÃO  1H',NULL,225.00,'',NULL,NULL,NULL,'2018-10-07 20:05:19',NULL,1,1,2,NULL),(4,'COMPRA DE SPRAY PARA MANUTENÇÃO',2,25.00,'',NULL,NULL,'2018-10-07 00:00:00','2018-10-07 20:22:48','spray para pintar peça do computador',3,2,2,NULL),(5,'FILTRO - PASSA BAIXA',2,256.99,'',NULL,'2018-10-25 00:00:00','2018-10-19 00:00:00','2018-10-19 20:56:10',NULL,2,2,2,NULL),(6,'FILTRO - PASSA BAIXA',2,256.99,'',NULL,NULL,NULL,'2018-10-19 20:56:29','stannis',2,2,2,NULL),(7,'PROJETO GUINDASTE',NULL,15000.00,'','7070senaoder70denovo','2018-10-19 00:00:00','2018-10-19 00:00:00','2018-10-19 21:03:28',NULL,4,4,2,NULL),(8,'TROCA DE FREIO',NULL,260.00,'',NULL,'2018-11-02 00:00:00','2018-11-02 00:00:00','2018-11-02 12:35:48',NULL,1,2,2,NULL),(9,'PINTURA',NULL,120.00,'',NULL,'2018-11-02 00:00:00','2018-11-02 00:00:00','2018-11-02 12:37:19',NULL,2,11,2,NULL),(10,'CASA',NULL,2.33,'\0',NULL,NULL,NULL,'2018-11-03 17:29:24',NULL,1,1,2,NULL),(11,'COMPRA DE PEÇA - ROSCA',10,100.00,'\0',NULL,'2018-11-03 00:00:00',NULL,'2018-11-03 20:23:15',NULL,3,11,2,NULL);
/*!40000 ALTER TABLE `lancamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `lancamentoestoque`
--

DROP TABLE IF EXISTS `lancamentoestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `lancamentoestoque` (
  `id` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `valor` decimal(12,2) NOT NULL,
  `quantidade` int(11) NOT NULL DEFAULT '1',
  `dataentrada` date DEFAULT NULL,
  `datasaida` date DEFAULT NULL,
  `validade` date DEFAULT NULL,
  `lote` varchar(45) DEFAULT NULL,
  `produto_id` int(11) NOT NULL,
  `contasaldo_id` int(11) NOT NULL,
  `estoque_id` int(11) NOT NULL,
  `usuario_id` int(11) DEFAULT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_estoqueproduto_produto1_idx` (`produto_id`),
  KEY `fk_estoqueproduto_contasaldo1_idx` (`contasaldo_id`),
  KEY `fk_lancamentoestoque_usuario1_idx` (`usuario_id`),
  KEY `fk_lancamentoestoque_estoque1_idx` (`estoque_id`),
  CONSTRAINT `fk_estoqueproduto_contasaldo1` FOREIGN KEY (`contasaldo_id`) REFERENCES `contasaldo` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_estoqueproduto_produto1` FOREIGN KEY (`produto_id`) REFERENCES `produto` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamentoestoque_estoque1` FOREIGN KEY (`estoque_id`) REFERENCES `estoque` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamentoestoque_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`) ON DELETE SET NULL ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=17 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lancamentoestoque`
--

LOCK TABLES `lancamentoestoque` WRITE;
/*!40000 ALTER TABLE `lancamentoestoque` DISABLE KEYS */;
INSERT INTO `lancamentoestoque` VALUES (3,24.20,22,'2018-08-27',NULL,NULL,NULL,2,2,3,NULL),(4,11.00,10,'2018-08-01',NULL,NULL,NULL,2,2,3,NULL),(5,15000.00,10,'2018-08-29',NULL,NULL,NULL,3,2,4,NULL),(6,4500.00,3,'2018-08-29',NULL,NULL,NULL,3,2,4,NULL),(7,10000.00,5,NULL,'2018-08-29',NULL,NULL,3,2,4,NULL),(8,4000.00,2,NULL,'2018-08-29',NULL,NULL,3,2,4,NULL),(9,7500.00,5,'2018-08-29',NULL,NULL,NULL,3,2,4,NULL),(10,3000.00,2,'2018-08-29',NULL,NULL,NULL,3,2,4,NULL),(11,6000.00,3,NULL,'2018-08-29',NULL,NULL,3,2,4,NULL),(12,52500.00,35,'2018-08-29',NULL,NULL,NULL,3,2,4,NULL),(13,552.00,80,'2018-09-07',NULL,NULL,NULL,6,2,6,NULL),(14,198.00,20,NULL,'2018-09-08',NULL,NULL,6,2,6,NULL),(15,26.00,50,'2018-09-23',NULL,NULL,NULL,7,2,7,NULL),(16,18.90,30,NULL,'2018-09-23',NULL,NULL,7,2,7,NULL);
/*!40000 ALTER TABLE `lancamentoestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `valorunitario` decimal(12,2) DEFAULT NULL,
  `custounitario` decimal(12,2) DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto`
--

LOCK TABLES `produto` WRITE;
/*!40000 ALTER TABLE `produto` DISABLE KEYS */;
INSERT INTO `produto` VALUES (2,'ROSCA ESTRELA 50MM X 50MM',5.20,1.10,''),(3,'PNEU MICHELLIN BURRACHUDO',2000.00,1500.00,''),(4,'PARMESAO',20.00,12.00,''),(5,'QUEIJO',2.59,1.10,''),(6,'CREME SEDA 500Ml',9.90,6.90,''),(7,'LÁPIS FABER CASTEL - EVOLUTION B',0.63,0.52,''),(8,'GARRAFINHA DE AGUA',0.60,2.50,'');
/*!40000 ALTER TABLE `produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtoemestoque`
--

DROP TABLE IF EXISTS `produtoemestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produtoemestoque` (
  `produto_id` int(11) NOT NULL,
  `estoque_id` int(11) NOT NULL,
  `quantidade` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`produto_id`,`estoque_id`),
  KEY `fk_produto_has_estoque_estoque1_idx` (`estoque_id`),
  KEY `fk_produto_has_estoque_produto1_idx` (`produto_id`),
  CONSTRAINT `fk_produto_has_estoque_estoque1` FOREIGN KEY (`estoque_id`) REFERENCES `estoque` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_produto_has_estoque_produto1` FOREIGN KEY (`produto_id`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtoemestoque`
--

LOCK TABLES `produtoemestoque` WRITE;
/*!40000 ALTER TABLE `produtoemestoque` DISABLE KEYS */;
INSERT INTO `produtoemestoque` VALUES (2,3,32),(3,4,42),(6,6,60),(7,7,20);
/*!40000 ALTER TABLE `produtoemestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `login` varchar(45) NOT NULL,
  `senha` varchar(256) NOT NULL,
  `cpf` varchar(20) NOT NULL,
  `permissao` varchar(1) NOT NULL,
  `email` varchar(45) NOT NULL,
  `telefone` varchar(45) DEFAULT NULL,
  `rua` varchar(45) DEFAULT NULL,
  `numero` int(11) DEFAULT NULL,
  `bairro` varchar(45) DEFAULT NULL,
  `cidade` varchar(45) DEFAULT NULL,
  `estado` varchar(45) DEFAULT NULL,
  `cep` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-30 20:43:05
