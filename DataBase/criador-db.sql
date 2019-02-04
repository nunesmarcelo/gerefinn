-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='TRADITIONAL,ALLOW_INVALID_DATES';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
-- -----------------------------------------------------
-- Schema financeirodb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema financeirodb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `financeirodb` DEFAULT CHARACTER SET utf8 ;
USE `financeirodb` ;

-- -----------------------------------------------------
-- Table `financeirodb`.`categoria`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`categoria` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`categoria` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(100) NOT NULL,
  `status` BIT(1) NOT NULL DEFAULT b'1',
  `tipo` ENUM('AC', 'ANC', 'PC', 'PNC', 'PL') NOT NULL,
  `descricao` VARCHAR(1000) NULL DEFAULT NULL,
  `rd` ENUM('R', 'D') NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`contasaldo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`contasaldo` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`contasaldo` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `banco` VARCHAR(45) NOT NULL,
  `saldo` DECIMAL(12,2) NOT NULL DEFAULT '0.00',
  `status` BIT(1) NOT NULL DEFAULT b'1',
  `agencia` VARCHAR(45) NULL DEFAULT NULL,
  `conta` VARCHAR(45) NULL DEFAULT NULL,
  `titular` VARCHAR(45) NULL DEFAULT NULL,
  `tipo` ENUM('CORRENTE', 'POUPANCA', 'MISTA', 'OUTRO') NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`instituicao`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`instituicao` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`instituicao` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `cnpj` VARCHAR(45) NULL DEFAULT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `telefone1` VARCHAR(45) NULL DEFAULT NULL,
  `telefone2` VARCHAR(45) NULL DEFAULT NULL,
  `responsavel` VARCHAR(45) NULL DEFAULT NULL,
  `rua` VARCHAR(45) NULL DEFAULT NULL,
  `numero` INT(11) NULL DEFAULT NULL,
  `bairro` VARCHAR(45) NULL DEFAULT NULL,
  `cidade` VARCHAR(45) NULL DEFAULT NULL,
  `estado` VARCHAR(45) NULL DEFAULT NULL,
  `cep` VARCHAR(45) NULL DEFAULT NULL,
  `fc` ENUM('F', 'C') NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`usuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`usuario` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`usuario` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `login` VARCHAR(45) NOT NULL,
  `senha` VARCHAR(256) NOT NULL,
  `cpf` VARCHAR(20) NOT NULL,
  `permissao` VARCHAR(1) NOT NULL,
  `email` VARCHAR(45) NOT NULL,
  `telefone` VARCHAR(45) NULL DEFAULT NULL,
  `rua` VARCHAR(45) NULL DEFAULT NULL,
  `numero` INT(11) NULL DEFAULT NULL,
  `bairro` VARCHAR(45) NULL DEFAULT NULL,
  `cidade` VARCHAR(45) NULL DEFAULT NULL,
  `estado` VARCHAR(45) NULL DEFAULT NULL,
  `cep` VARCHAR(45) NULL DEFAULT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`lancamento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`lancamento` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`lancamento` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `descricao` VARCHAR(45) NOT NULL,
  `valortotal` DECIMAL(12,2) NOT NULL,
  `pago` BIT(1) NOT NULL DEFAULT b'0',
  `datavencimento` DATETIME NULL DEFAULT NULL,
  `datacadastro` DATETIME NOT NULL,
  `observacao` VARCHAR(500) NULL DEFAULT NULL,
  `contasaldo_id` INT(7) NOT NULL,
  `categoria_id` INT(7) NOT NULL,
  `usuario_id` INT(7) NOT NULL,
  `instituicao_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_lancamento_contasaldo1_idx` (`contasaldo_id` ASC),
  INDEX `fk_lancamento_categoria1_idx` (`categoria_id` ASC),
  INDEX `fk_lancamento_usuario1_idx` (`usuario_id` ASC),
  INDEX `fk_lancamento_instituicao1_idx` (`instituicao_id` ASC),
  CONSTRAINT `fk_lancamento_categoria1`
    FOREIGN KEY (`categoria_id`)
    REFERENCES `financeirodb`.`categoria` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_contasaldo1`
    FOREIGN KEY (`contasaldo_id`)
    REFERENCES `financeirodb`.`contasaldo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_instituicao1`
    FOREIGN KEY (`instituicao_id`)
    REFERENCES `financeirodb`.`instituicao` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_lancamento_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `financeirodb`.`usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`caixa`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`caixa` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`caixa` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `valorabertura` VARCHAR(45) NOT NULL,
  `valorfechamento` VARCHAR(45) NULL DEFAULT NULL,
  `horaabertura` DATETIME NOT NULL,
  `horafechamento` DATETIME NULL DEFAULT NULL,
  `lancamento_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_caixa_lancamento1_idx` (`lancamento_id` ASC),
  CONSTRAINT `fk_caixa_lancamento1`
    FOREIGN KEY (`lancamento_id`)
    REFERENCES `financeirodb`.`lancamento` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`estoque`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`estoque` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`estoque` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `responsavel` VARCHAR(45) NULL DEFAULT NULL,
  `telefone` VARCHAR(45) NULL DEFAULT NULL,
  `email` VARCHAR(45) NULL DEFAULT NULL,
  `rua` VARCHAR(45) NULL DEFAULT NULL,
  `numero` INT(6) NULL DEFAULT NULL,
  `bairro` VARCHAR(45) NULL DEFAULT NULL,
  `cidade` VARCHAR(45) NULL DEFAULT NULL,
  `estado` VARCHAR(45) NULL DEFAULT NULL,
  `cep` VARCHAR(45) NULL DEFAULT NULL,
  `status` BIT(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`imagemcontasaldo`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`imagemcontasaldo` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`imagemcontasaldo` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `principal` BIT(1) NOT NULL DEFAULT b'0',
  `contasaldo_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`, `contasaldo_id`),
  INDEX `fk_imagemcontasaldo_contasaldo1_idx` (`contasaldo_id` ASC),
  CONSTRAINT `fk_imagemcontasaldo_contasaldo1`
    FOREIGN KEY (`contasaldo_id`)
    REFERENCES `financeirodb`.`contasaldo` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`imagemestoque`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`imagemestoque` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`imagemestoque` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `principal` BIT(1) NOT NULL DEFAULT b'0',
  `estoque_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`, `estoque_id`),
  INDEX `fk_imagemestoque_estoque1_idx` (`estoque_id` ASC),
  CONSTRAINT `fk_imagemestoque_estoque1`
    FOREIGN KEY (`estoque_id`)
    REFERENCES `financeirodb`.`estoque` (`id`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`imageminstituicao`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`imageminstituicao` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`imageminstituicao` (
  `id` INT(11) NOT NULL,
  `nome` VARCHAR(200) NOT NULL,
  `principal` BIT(1) NOT NULL DEFAULT b'0',
  `instituicao_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`, `instituicao_id`),
  INDEX `fk_imageminstituicao_instituicao1_idx` (`instituicao_id` ASC),
  CONSTRAINT `fk_imageminstituicao_instituicao1`
    FOREIGN KEY (`instituicao_id`)
    REFERENCES `financeirodb`.`instituicao` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`produto`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`produto` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`produto` (
  `id` INT(7) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `valorunitario` DECIMAL(12,2) NULL DEFAULT NULL,
  `custounitario` DECIMAL(12,2) NULL DEFAULT NULL,
  `estoqueminimo` INT(7) NULL DEFAULT NULL,
  `status` BIT(1) NOT NULL DEFAULT b'1',
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`imagemproduto`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`imagemproduto` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`imagemproduto` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `principal` BIT(1) NOT NULL DEFAULT b'0',
  `produto_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`, `produto_id`),
  INDEX `fk_imagemproduto_produto1_idx` (`produto_id` ASC),
  CONSTRAINT `fk_imagemproduto_produto1`
    FOREIGN KEY (`produto_id`)
    REFERENCES `financeirodb`.`produto` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`imagemusuario`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`imagemusuario` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`imagemusuario` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(200) NOT NULL,
  `principal` BIT(1) NOT NULL DEFAULT b'0',
  `usuario_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`, `usuario_id`),
  INDEX `fk_imagemusuario_usuario1_idx` (`usuario_id` ASC),
  CONSTRAINT `fk_imagemusuario_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `financeirodb`.`usuario` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`logcaixa`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`logcaixa` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`logcaixa` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `tipo` ENUM('SANGRIA', 'REFORCO') NOT NULL,
  `valor` DECIMAL(12,2) NOT NULL,
  `horario` DATETIME NOT NULL,
  `motivo` VARCHAR(200) NULL DEFAULT NULL,
  `responsavel` VARCHAR(45) NULL DEFAULT NULL,
  `caixa_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_logcaixa_caixa1_idx` (`caixa_id` ASC),
  CONSTRAINT `fk_logcaixa_caixa1`
    FOREIGN KEY (`caixa_id`)
    REFERENCES `financeirodb`.`caixa` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`notafiscallancamento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`notafiscallancamento` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`notafiscallancamento` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `numero` VARCHAR(45) NOT NULL,
  `data` DATETIME NOT NULL,
  `lancamento_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_notafiscallancamento_lancamento1_idx` (`lancamento_id` ASC),
  CONSTRAINT `fk_notafiscallancamento_lancamento1`
    FOREIGN KEY (`lancamento_id`)
    REFERENCES `financeirodb`.`lancamento` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`venda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`venda` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`venda` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `concluida` BIT(1) NULL DEFAULT NULL,
  `valortotal` DECIMAL(12,2) NULL DEFAULT NULL,
  `caixa_id` INT(11) NOT NULL,
  `usuario_id` INT(7) NOT NULL,
  `instituicao_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_venda_caixa1_idx` (`caixa_id` ASC),
  INDEX `fk_venda_instituicao1_idx` (`instituicao_id` ASC),
  INDEX `fk_venda_usuario1_idx` (`usuario_id` ASC),
  CONSTRAINT `fk_venda_caixa1`
    FOREIGN KEY (`caixa_id`)
    REFERENCES `financeirodb`.`caixa` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_venda_instituicao1`
    FOREIGN KEY (`instituicao_id`)
    REFERENCES `financeirodb`.`instituicao` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_venda_usuario1`
    FOREIGN KEY (`usuario_id`)
    REFERENCES `financeirodb`.`usuario` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`notafiscalvenda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`notafiscalvenda` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`notafiscalvenda` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `numero` VARCHAR(45) NOT NULL,
  `data` DATETIME NOT NULL,
  `venda_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_notafiscalvenda_venda1_idx` (`venda_id` ASC),
  CONSTRAINT `fk_notafiscalvenda_venda1`
    FOREIGN KEY (`venda_id`)
    REFERENCES `financeirodb`.`venda` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`tipopagamento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`tipopagamento` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`tipopagamento` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `nome` VARCHAR(45) NOT NULL,
  `parcelasmaximas` INT(7) NOT NULL DEFAULT '0',
  PRIMARY KEY (`id`))
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`pagamento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`pagamento` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`pagamento` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `valor` DECIMAL(12,2) NOT NULL,
  `parcelas` INT(7) NOT NULL DEFAULT '0',
  `datainicio` DATE NULL DEFAULT NULL,
  `tipopagamento_id` INT(11) NOT NULL,
  `venda_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_pagamento_tipopagamento1_idx` (`tipopagamento_id` ASC),
  INDEX `fk_pagamento_venda1_idx` (`venda_id` ASC),
  CONSTRAINT `fk_pagamento_tipopagamento1`
    FOREIGN KEY (`tipopagamento_id`)
    REFERENCES `financeirodb`.`tipopagamento` (`id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_pagamento_venda1`
    FOREIGN KEY (`venda_id`)
    REFERENCES `financeirodb`.`venda` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`produtoemestoque`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`produtoemestoque` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`produtoemestoque` (
  `produto_id` INT(7) NOT NULL AUTO_INCREMENT,
  `estoque_id` INT(7) NOT NULL,
  `quantidade` INT(11) NOT NULL DEFAULT '0',
  PRIMARY KEY (`produto_id`, `estoque_id`),
  INDEX `fk_produto_has_estoque_estoque1_idx` (`estoque_id` ASC),
  INDEX `fk_produto_has_estoque_produto1_idx` (`produto_id` ASC),
  CONSTRAINT `fk_produto_has_estoque_estoque1`
    FOREIGN KEY (`estoque_id`)
    REFERENCES `financeirodb`.`estoque` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_produto_has_estoque_produto1`
    FOREIGN KEY (`produto_id`)
    REFERENCES `financeirodb`.`produto` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`produtolancamento`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`produtolancamento` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`produtolancamento` (
  `produto_id` INT(11) NOT NULL AUTO_INCREMENT,
  `estoque_id` INT(11) NOT NULL,
  `lancamento_id` INT(11) NOT NULL,
  `validade` DATE NULL DEFAULT NULL,
  `lote` VARCHAR(45) NULL DEFAULT NULL,
  `quantidade` INT(10) NULL DEFAULT NULL,
  `valor` DECIMAL(12,2) NULL DEFAULT NULL,
  PRIMARY KEY (`produto_id`, `estoque_id`, `lancamento_id`),
  INDEX `fk_produto_has_estoque_estoque2_idx` (`estoque_id` ASC),
  INDEX `fk_produto_has_estoque_produto2_idx` (`produto_id` ASC),
  INDEX `fk_produtolancamento_lancamento1_idx` (`lancamento_id` ASC),
  CONSTRAINT `fk_produto_has_estoque_estoque2`
    FOREIGN KEY (`estoque_id`)
    REFERENCES `financeirodb`.`estoque` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_produto_has_estoque_produto2`
    FOREIGN KEY (`produto_id`)
    REFERENCES `financeirodb`.`produto` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE,
  CONSTRAINT `fk_produtolancamento_lancamento1`
    FOREIGN KEY (`lancamento_id`)
    REFERENCES `financeirodb`.`lancamento` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `financeirodb`.`produtovenda`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `financeirodb`.`produtovenda` ;

CREATE TABLE IF NOT EXISTS `financeirodb`.`produtovenda` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `quantidade` INT(11) NOT NULL DEFAULT '1',
  `valor` DECIMAL(12,2) NOT NULL DEFAULT '0.00',
  `desconto` DECIMAL(12,2) NULL DEFAULT NULL,
  `venda_id` INT(11) NOT NULL,
  `produtoemestoque_produto_id` INT(7) NOT NULL,
  `produtoemestoque_estoque_id` INT(7) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `fk_produtovenda_venda1_idx` (`venda_id` ASC),
  INDEX `fk_produtovenda_produtoemestoque1_idx` (`produtoemestoque_produto_id` ASC, `produtoemestoque_estoque_id` ASC),
  CONSTRAINT `fk_produtovenda_produtoemestoque1`
    FOREIGN KEY (`produtoemestoque_produto_id` , `produtoemestoque_estoque_id`)
    REFERENCES `financeirodb`.`produtoemestoque` (`produto_id` , `estoque_id`)
    ON DELETE NO ACTION
    ON UPDATE CASCADE,
  CONSTRAINT `fk_produtovenda_venda1`
    FOREIGN KEY (`venda_id`)
    REFERENCES `financeirodb`.`venda` (`id`)
    ON DELETE CASCADE
    ON UPDATE CASCADE)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
