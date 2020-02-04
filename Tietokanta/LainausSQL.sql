SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`Lainaaja`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Lainaaja` (
  `LainaajaID` INT NOT NULL AUTO_INCREMENT,
  `Etunimi` VARCHAR(45) NULL,
  `Sukunimi` VARCHAR(45) NULL,
  `Luokka` VARCHAR(45) NULL,
  PRIMARY KEY (`LainaajaID`),
  UNIQUE INDEX `LainaajaID_UNIQUE` (`LainaajaID` ASC) )
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Lainaus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Lainaus` (
  `LaianusID` INT NOT NULL AUTO_INCREMENT,
  `Lainaaja_LainaajaID` INT NOT NULL,
  `Välineet_VäineID` INT NOT NULL,
  PRIMARY KEY (`LaianusID`, `Lainaaja_LainaajaID`, `Välineet_VäineID`),
  UNIQUE INDEX `LaianusID_UNIQUE` (`LaianusID` ASC) ,
  INDEX `fk_Lainaus_Lainaaja_idx` (`Lainaaja_LainaajaID` ASC) ,
  INDEX `fk_Lainaus_Välineet1_idx` (`Välineet_VäineID` ASC) ,
  CONSTRAINT `fk_Lainaus_Lainaaja`
    FOREIGN KEY (`Lainaaja_LainaajaID`)
    REFERENCES `mydb`.`Lainaaja` (`LainaajaID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Lainaus_Välineet1`
    FOREIGN KEY (`Välineet_VäineID`)
    REFERENCES `mydb`.`Välineet` (`VäineID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Välineet`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Välineet` (
  `VäineID` INT NOT NULL AUTO_INCREMENT,
  `Nimi` VARCHAR(45) NULL,
  `ID` VARCHAR(45) NULL,
  PRIMARY KEY (`VäineID`),
  UNIQUE INDEX `VäineID_UNIQUE` (`VäineID` ASC) )
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
