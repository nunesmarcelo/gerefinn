-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: 127.0.0.1    Database: financeirodb
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
-- Table structure for table `caixa`
--

DROP TABLE IF EXISTS `caixa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `caixa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `valorabertura` decimal(12,2) NOT NULL,
  `valorfechamento` decimal(12,2) DEFAULT NULL,
  `horaabertura` datetime NOT NULL,
  `horafechamento` datetime DEFAULT NULL,
  `lancamento_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_caixa_lancamento1_idx` (`lancamento_id`),
  CONSTRAINT `fk_caixa_lancamento1` FOREIGN KEY (`lancamento_id`) REFERENCES `lancamento` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `caixa`
--

LOCK TABLES `caixa` WRITE;
/*!40000 ALTER TABLE `caixa` DISABLE KEYS */;
INSERT INTO `caixa` VALUES (5,0.00,0.00,'2019-01-13 20:17:32','2019-01-13 20:32:48',10),(6,0.00,21.00,'2019-01-13 20:36:20','2019-01-23 10:39:39',11),(7,21.00,50.00,'2019-01-23 10:40:31','2019-01-23 10:40:36',12),(8,50.00,50.00,'2019-01-23 11:07:51','2019-01-23 11:26:21',13),(9,50.00,50.00,'2019-01-23 11:26:41','2019-01-23 11:41:00',14),(10,50.00,50.00,'2019-01-23 11:41:49','2019-01-23 13:12:52',15),(11,50.00,50.00,'2019-01-23 13:13:13','2019-01-25 18:21:24',16),(12,50.00,50.00,'2019-01-25 18:21:37','2019-01-29 10:26:53',17),(13,50.00,NULL,'2019-01-29 10:28:09',NULL,1);
/*!40000 ALTER TABLE `caixa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `categoria`
--

DROP TABLE IF EXISTS `categoria`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `categoria` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
  `nome` varchar(100) NOT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  `tipo` enum('AC','ANC','PC','PNC','PL') NOT NULL,
  `descricao` varchar(1000) DEFAULT NULL,
  `rd` enum('R','D') NOT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `categoria`
--

LOCK TABLES `categoria` WRITE;
/*!40000 ALTER TABLE `categoria` DISABLE KEYS */;
INSERT INTO `categoria` VALUES (1,'PEÇAS','','PC','teste','D'),(3,'SERVIÇO MANUTENÇÃO','','ANC','SErviços prestados','R'),(4,'TESTE','','PNC','aa','D'),(6,'ÁGUA','','PC',NULL,'D'),(7,'PRODUÇÃO','','AC','teste','R'),(8,'ENTRADA - SÓCIOS','','PL','investimentos','R'),(9,'CAIXA','','AC','caixa para vendas','R');
/*!40000 ALTER TABLE `categoria` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `contasaldo`
--

DROP TABLE IF EXISTS `contasaldo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `contasaldo` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `banco` varchar(45) DEFAULT NULL,
  `saldo` decimal(12,2) NOT NULL DEFAULT '0.00',
  `status` bit(1) NOT NULL DEFAULT b'1',
  `agencia` varchar(45) DEFAULT NULL,
  `conta` varchar(45) DEFAULT NULL,
  `titular` varchar(45) DEFAULT NULL,
  `tipo` enum('CORRENTE','POUPANCA','MISTA','OUTRO') DEFAULT NULL,
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `contasaldo`
--

LOCK TABLES `contasaldo` WRITE;
/*!40000 ALTER TABLE `contasaldo` DISABLE KEYS */;
INSERT INTO `contasaldo` VALUES (1,'brasil - ag 3440','BRASIL',1000.00,'','3440','01111-9','JOseph','MISTA'),(3,'CAIXA',NULL,250.00,'',NULL,NULL,NULL,'OUTRO');
/*!40000 ALTER TABLE `contasaldo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `estoque`
--

DROP TABLE IF EXISTS `estoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `estoque` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
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
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `estoque`
--

LOCK TABLES `estoque` WRITE;
/*!40000 ALTER TABLE `estoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `estoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagemcontasaldo`
--

DROP TABLE IF EXISTS `imagemcontasaldo`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imagemcontasaldo` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `principal` bit(1) NOT NULL DEFAULT b'0',
  `contasaldo_id` int(7) NOT NULL,
  PRIMARY KEY (`id`,`contasaldo_id`),
  KEY `fk_imagemcontasaldo_contasaldo1_idx` (`contasaldo_id`),
  CONSTRAINT `fk_imagemcontasaldo_contasaldo1` FOREIGN KEY (`contasaldo_id`) REFERENCES `contasaldo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagemcontasaldo`
--

LOCK TABLES `imagemcontasaldo` WRITE;
/*!40000 ALTER TABLE `imagemcontasaldo` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagemcontasaldo` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagemestoque`
--

DROP TABLE IF EXISTS `imagemestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imagemestoque` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `principal` bit(1) NOT NULL DEFAULT b'0',
  `estoque_id` int(7) NOT NULL,
  PRIMARY KEY (`id`,`estoque_id`),
  KEY `fk_imagemestoque_estoque1_idx` (`estoque_id`),
  CONSTRAINT `fk_imagemestoque_estoque1` FOREIGN KEY (`estoque_id`) REFERENCES `estoque` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagemestoque`
--

LOCK TABLES `imagemestoque` WRITE;
/*!40000 ALTER TABLE `imagemestoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagemestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imageminstituicao`
--

DROP TABLE IF EXISTS `imageminstituicao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imageminstituicao` (
  `id` int(11) NOT NULL,
  `nome` varchar(200) NOT NULL,
  `principal` bit(1) NOT NULL DEFAULT b'0',
  `instituicao_id` int(7) NOT NULL,
  PRIMARY KEY (`id`,`instituicao_id`),
  KEY `fk_imageminstituicao_instituicao1_idx` (`instituicao_id`),
  CONSTRAINT `fk_imageminstituicao_instituicao1` FOREIGN KEY (`instituicao_id`) REFERENCES `instituicao` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imageminstituicao`
--

LOCK TABLES `imageminstituicao` WRITE;
/*!40000 ALTER TABLE `imageminstituicao` DISABLE KEYS */;
/*!40000 ALTER TABLE `imageminstituicao` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagemproduto`
--

DROP TABLE IF EXISTS `imagemproduto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imagemproduto` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `principal` bit(1) NOT NULL DEFAULT b'0',
  `produto_id` int(7) NOT NULL,
  PRIMARY KEY (`id`,`produto_id`),
  KEY `fk_imagemproduto_produto1_idx` (`produto_id`),
  CONSTRAINT `fk_imagemproduto_produto1` FOREIGN KEY (`produto_id`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagemproduto`
--

LOCK TABLES `imagemproduto` WRITE;
/*!40000 ALTER TABLE `imagemproduto` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagemproduto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `imagemusuario`
--

DROP TABLE IF EXISTS `imagemusuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `imagemusuario` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(200) NOT NULL,
  `principal` bit(1) NOT NULL DEFAULT b'0',
  `usuario_id` int(7) NOT NULL,
  PRIMARY KEY (`id`,`usuario_id`),
  KEY `fk_imagemusuario_usuario1_idx` (`usuario_id`),
  CONSTRAINT `fk_imagemusuario_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `imagemusuario`
--

LOCK TABLES `imagemusuario` WRITE;
/*!40000 ALTER TABLE `imagemusuario` DISABLE KEYS */;
/*!40000 ALTER TABLE `imagemusuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `instituicao`
--

DROP TABLE IF EXISTS `instituicao`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `instituicao` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
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
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `instituicao`
--

LOCK TABLES `instituicao` WRITE;
/*!40000 ALTER TABLE `instituicao` DISABLE KEYS */;
INSERT INTO `instituicao` VALUES (1,'TADEU','11.111.111/1111-11','marcelo.x1@hotmail.com','(31) 3333-33332','(31) 9965-98350',NULL,'...',1,'...','...','...','35.700-312','C'),(4,'JOA','00.155.200/5511-11','joaquim12','(31) 55155-5588','(31) 3775-5885','JOA','...',2,'...','...','MG','35.700-318','C'),(5,'casa de peças união','22.052.110/0100-25','pecasuniao@gmail.com','(31) 3372-3382','(31) 3775-5885','carlos',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(6,'CAIXA','11.111.111/1222-22','octavio7l@hotmail.com','(31) 3372-3382','(31) 3775-5885','Fernando',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(7,'MATHIAS','12.733.222/0001-13','mathiassantos@yahoo.com','(31) 3776-5991','(31) 3327-2278','MATHIAS',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(8,'ÁGUA 7L','33.944.202/1000-23','ijunior@ijunior.com.br','(31) 3372-3382','(31) 99659-8350','ANA',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(9,'FERNANDO','22.344.990/0001-23','fergontransportes@hotmail.com','(31) 55155-5588',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'C'),(10,'MINERITA','45.033.392/1000-23','mineritamg@gmail.com','(31) 3221-6557',NULL,'ROMILDA',NULL,NULL,NULL,NULL,NULL,NULL,'C'),(11,'AUTO MOLAS IMPERATRIZ','28.393.322/1000-29','atendimento@imperatriz.com','(31) 3776-5991','(31) 3327-2278','THAIS',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(12,'BORRACHARIA SANTA MARIA','78.695.382/1000-20','santamaria@gmail.com','(31) 99334-4596','(31) 3493-2283','GERALDO',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(13,'CASA DA BORRACHA','28.338.442/8112-28','borracha@7l.com','(31) 33333-3332','(11) 2525-6255','LUCIANA',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(14,'ITAIPU PEÇAS','33.829.301/9182-22','atendimento@itaipumg.com','(31) 3233-4421','(34) 3455-2122','MARCOS',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(15,'MATERIAIS LUCENA','37.337.262/5129-91','lucena@gmail.com','(31) 3776-5985','(31) 3322-1212','JORGE',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(16,'SERVIÇO MANUTENÇÃO','25.123.321/5552-01','manutencao@cia.com','(31) 3562-2584',NULL,'TAINARA',NULL,NULL,NULL,NULL,NULL,NULL,'F'),(17,'ELETRONICOS MINAS GERAIS','22.218.392/0339-32','eletromg@hotmail.com','(31) 3776-5985','(34) 3212-4423','MICHELLE',NULL,NULL,NULL,NULL,NULL,NULL,'F');
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
  `valortotal` decimal(12,2) NOT NULL,
  `pago` bit(1) NOT NULL DEFAULT b'0',
  `datavencimento` datetime DEFAULT NULL,
  `datacadastro` datetime NOT NULL,
  `observacao` varchar(500) DEFAULT NULL,
  `contasaldo_id` int(7) NOT NULL,
  `categoria_id` int(7) NOT NULL,
  `usuario_id` int(7) NOT NULL,
  `instituicao_id` int(7) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_lancamento_contasaldo1_idx` (`contasaldo_id`),
  KEY `fk_lancamento_categoria1_idx` (`categoria_id`),
  KEY `fk_lancamento_usuario1_idx` (`usuario_id`),
  KEY `fk_lancamento_instituicao1_idx` (`instituicao_id`),
  CONSTRAINT `fk_lancamento_categoria1` FOREIGN KEY (`categoria_id`) REFERENCES `categoria` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_contasaldo1` FOREIGN KEY (`contasaldo_id`) REFERENCES `contasaldo` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_instituicao1` FOREIGN KEY (`instituicao_id`) REFERENCES `instituicao` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `lancamento`
--

LOCK TABLES `lancamento` WRITE;
/*!40000 ALTER TABLE `lancamento` DISABLE KEYS */;
INSERT INTO `lancamento` VALUES (1,'teste',500.00,'\0','2018-09-09 10:20:00','2018-09-09 10:10:00','teste',1,1,1,1),(3,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:10:05','2019-01-13 20:10:05',NULL,3,9,1,6),(4,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:12:49','2019-01-13 20:12:49',NULL,3,9,1,6),(5,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:16:07','2019-01-13 20:16:07',NULL,3,9,1,6),(6,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:17:36','2019-01-13 20:17:36',NULL,3,9,1,6),(7,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:17:41','2019-01-13 20:17:41',NULL,3,9,1,6),(8,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:23:41','2019-01-13 20:23:41',NULL,3,9,1,6),(9,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:25:58','2019-01-13 20:25:58',NULL,3,9,1,6),(10,'FECHAMENTO CAIXA - ',0.00,'','2019-01-13 20:32:49','2019-01-13 20:32:49',NULL,3,9,1,6),(11,'FECHAMENTO CAIXA - ',21.00,'','2019-01-23 10:39:42','2019-01-23 10:39:42',NULL,3,9,1,6),(12,'FECHAMENTO CAIXA - ',29.00,'','2019-01-23 10:40:44','2019-01-23 10:40:44',NULL,3,9,1,6),(13,'FECHAMENTO CAIXA - ',0.00,'','2019-01-23 11:26:22','2019-01-23 11:26:22',NULL,3,9,1,6),(14,'FECHAMENTO CAIXA - ',0.00,'','2019-01-23 11:41:01','2019-01-23 11:41:01',NULL,3,9,1,6),(15,'FECHAMENTO CAIXA - ',0.00,'','2019-01-23 13:12:58','2019-01-23 13:12:58',NULL,3,9,1,6),(16,'FECHAMENTO CAIXA - ',0.00,'','2019-01-25 18:21:27','2019-01-25 18:21:27',NULL,3,9,1,6),(17,'FECHAMENTO CAIXA - ',0.00,'','2019-01-29 10:26:54','2019-01-29 10:26:54',NULL,3,9,1,6);
/*!40000 ALTER TABLE `lancamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `logcaixa`
--

DROP TABLE IF EXISTS `logcaixa`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `logcaixa` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `tipo` enum('SANGRIA','REFORCO') NOT NULL,
  `valor` decimal(12,2) NOT NULL,
  `horario` datetime NOT NULL,
  `motivo` varchar(200) DEFAULT NULL,
  `responsavel` varchar(45) DEFAULT NULL,
  `caixa_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_logcaixa_caixa1_idx` (`caixa_id`),
  CONSTRAINT `fk_logcaixa_caixa1` FOREIGN KEY (`caixa_id`) REFERENCES `caixa` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `logcaixa`
--

LOCK TABLES `logcaixa` WRITE;
/*!40000 ALTER TABLE `logcaixa` DISABLE KEYS */;
/*!40000 ALTER TABLE `logcaixa` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notafiscallancamento`
--

DROP TABLE IF EXISTS `notafiscallancamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notafiscallancamento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `numero` varchar(45) NOT NULL,
  `data` datetime NOT NULL,
  `lancamento_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_notafiscallancamento_lancamento1_idx` (`lancamento_id`),
  CONSTRAINT `fk_notafiscallancamento_lancamento1` FOREIGN KEY (`lancamento_id`) REFERENCES `lancamento` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notafiscallancamento`
--

LOCK TABLES `notafiscallancamento` WRITE;
/*!40000 ALTER TABLE `notafiscallancamento` DISABLE KEYS */;
/*!40000 ALTER TABLE `notafiscallancamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `notafiscalvenda`
--

DROP TABLE IF EXISTS `notafiscalvenda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `notafiscalvenda` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `numero` varchar(45) NOT NULL,
  `data` datetime NOT NULL,
  `venda_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_notafiscalvenda_venda1_idx` (`venda_id`),
  CONSTRAINT `fk_notafiscalvenda_venda1` FOREIGN KEY (`venda_id`) REFERENCES `venda` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `notafiscalvenda`
--

LOCK TABLES `notafiscalvenda` WRITE;
/*!40000 ALTER TABLE `notafiscalvenda` DISABLE KEYS */;
/*!40000 ALTER TABLE `notafiscalvenda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `pagamento`
--

DROP TABLE IF EXISTS `pagamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `pagamento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `valor` decimal(12,2) NOT NULL,
  `parcelas` int(7) NOT NULL DEFAULT '0',
  `datainicio` date DEFAULT NULL,
  `tipopagamento_id` int(11) NOT NULL,
  `venda_id` int(11) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_pagamento_tipopagamento1_idx` (`tipopagamento_id`),
  KEY `fk_pagamento_venda1_idx` (`venda_id`),
  CONSTRAINT `fk_pagamento_tipopagamento1` FOREIGN KEY (`tipopagamento_id`) REFERENCES `tipopagamento` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_pagamento_venda1` FOREIGN KEY (`venda_id`) REFERENCES `venda` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `pagamento`
--

LOCK TABLES `pagamento` WRITE;
/*!40000 ALTER TABLE `pagamento` DISABLE KEYS */;
/*!40000 ALTER TABLE `pagamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produto`
--

DROP TABLE IF EXISTS `produto`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produto` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `valorunitario` decimal(12,2) DEFAULT NULL,
  `custounitario` decimal(12,2) DEFAULT NULL,
  `estoqueminimo` int(7) DEFAULT NULL,
  `status` bit(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produto`
--

LOCK TABLES `produto` WRITE;
/*!40000 ALTER TABLE `produto` DISABLE KEYS */;
INSERT INTO `produto` VALUES (3,'banana',5.20,1.29,1,'\0'),(5,'teste',2000.00,1.29,1,'\0');
/*!40000 ALTER TABLE `produto` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtoemestoque`
--

DROP TABLE IF EXISTS `produtoemestoque`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produtoemestoque` (
  `produto_id` int(7) NOT NULL AUTO_INCREMENT,
  `estoque_id` int(7) NOT NULL,
  `quantidade` int(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`produto_id`,`estoque_id`),
  KEY `fk_produto_has_estoque_estoque1_idx` (`estoque_id`),
  KEY `fk_produto_has_estoque_produto1_idx` (`produto_id`),
  CONSTRAINT `fk_produto_has_estoque_estoque1` FOREIGN KEY (`estoque_id`) REFERENCES `estoque` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_produto_has_estoque_produto1` FOREIGN KEY (`produto_id`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtoemestoque`
--

LOCK TABLES `produtoemestoque` WRITE;
/*!40000 ALTER TABLE `produtoemestoque` DISABLE KEYS */;
/*!40000 ALTER TABLE `produtoemestoque` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtolancamento`
--

DROP TABLE IF EXISTS `produtolancamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produtolancamento` (
  `produto_id` int(11) NOT NULL AUTO_INCREMENT,
  `estoque_id` int(11) NOT NULL,
  `lancamento_id` int(11) NOT NULL,
  `validade` date DEFAULT NULL,
  `lote` varchar(45) DEFAULT NULL,
  `quantidade` int(10) DEFAULT NULL,
  `valor` decimal(12,2) DEFAULT NULL,
  PRIMARY KEY (`produto_id`,`estoque_id`,`lancamento_id`),
  KEY `fk_produto_has_estoque_estoque2_idx` (`estoque_id`),
  KEY `fk_produto_has_estoque_produto2_idx` (`produto_id`),
  KEY `fk_produtolancamento_lancamento1_idx` (`lancamento_id`),
  CONSTRAINT `fk_produto_has_estoque_estoque2` FOREIGN KEY (`estoque_id`) REFERENCES `estoque` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_produto_has_estoque_produto2` FOREIGN KEY (`produto_id`) REFERENCES `produto` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_produtolancamento_lancamento1` FOREIGN KEY (`lancamento_id`) REFERENCES `lancamento` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtolancamento`
--

LOCK TABLES `produtolancamento` WRITE;
/*!40000 ALTER TABLE `produtolancamento` DISABLE KEYS */;
/*!40000 ALTER TABLE `produtolancamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `produtovenda`
--

DROP TABLE IF EXISTS `produtovenda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `produtovenda` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `quantidade` int(11) NOT NULL DEFAULT '1',
  `valor` decimal(12,2) NOT NULL DEFAULT '0.00',
  `desconto` decimal(12,2) DEFAULT NULL,
  `venda_id` int(11) NOT NULL,
  `produtoemestoque_produto_id` int(7) NOT NULL,
  `produtoemestoque_estoque_id` int(7) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_produtovenda_venda1_idx` (`venda_id`),
  KEY `fk_produtovenda_produtoemestoque1_idx` (`produtoemestoque_produto_id`,`produtoemestoque_estoque_id`),
  CONSTRAINT `fk_produtovenda_produtoemestoque1` FOREIGN KEY (`produtoemestoque_produto_id`, `produtoemestoque_estoque_id`) REFERENCES `produtoemestoque` (`produto_id`, `estoque_id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_produtovenda_venda1` FOREIGN KEY (`venda_id`) REFERENCES `venda` (`id`) ON DELETE CASCADE ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `produtovenda`
--

LOCK TABLES `produtovenda` WRITE;
/*!40000 ALTER TABLE `produtovenda` DISABLE KEYS */;
/*!40000 ALTER TABLE `produtovenda` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `tipopagamento`
--

DROP TABLE IF EXISTS `tipopagamento`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tipopagamento` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `nome` varchar(45) NOT NULL,
  `parcelasmaximas` int(7) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tipopagamento`
--

LOCK TABLES `tipopagamento` WRITE;
/*!40000 ALTER TABLE `tipopagamento` DISABLE KEYS */;
/*!40000 ALTER TABLE `tipopagamento` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `usuario`
--

DROP TABLE IF EXISTS `usuario`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `usuario` (
  `id` int(7) NOT NULL AUTO_INCREMENT,
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
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `usuario`
--

LOCK TABLES `usuario` WRITE;
/*!40000 ALTER TABLE `usuario` DISABLE KEYS */;
INSERT INTO `usuario` VALUES (1,'MARCELO','marcelo','nunes','12783229650','A','teste@teste.com',NULL,NULL,NULL,NULL,NULL,NULL,NULL);
/*!40000 ALTER TABLE `usuario` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `venda`
--

DROP TABLE IF EXISTS `venda`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `venda` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `concluida` bit(1) NOT NULL DEFAULT b'1',
  `valortotal` decimal(12,2) DEFAULT NULL,
  `caixa_id` int(11) NOT NULL,
  `usuario_id` int(7) NOT NULL,
  `instituicao_id` int(7) NOT NULL,
  PRIMARY KEY (`id`),
  KEY `fk_venda_caixa1_idx` (`caixa_id`),
  KEY `fk_venda_instituicao1_idx` (`instituicao_id`),
  KEY `fk_venda_usuario1_idx` (`usuario_id`),
  CONSTRAINT `fk_venda_caixa1` FOREIGN KEY (`caixa_id`) REFERENCES `caixa` (`id`) ON DELETE CASCADE ON UPDATE CASCADE,
  CONSTRAINT `fk_venda_instituicao1` FOREIGN KEY (`instituicao_id`) REFERENCES `instituicao` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE,
  CONSTRAINT `fk_venda_usuario1` FOREIGN KEY (`usuario_id`) REFERENCES `usuario` (`id`) ON DELETE NO ACTION ON UPDATE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `venda`
--

LOCK TABLES `venda` WRITE;
/*!40000 ALTER TABLE `venda` DISABLE KEYS */;
/*!40000 ALTER TABLE `venda` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2019-01-30 20:42:31
