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
-- Table `mydb`.`Materiaali`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Materiaali` (
  `MateriaaliID` INT NOT NULL AUTO_INCREMENT,
  `Nimi` VARCHAR(45) NULL,
  `Koko` VARCHAR(45) NULL,
  `Hinta` DECIMAL NULL,
  `Määrä` INT NULL,
  PRIMARY KEY (`MateriaaliID`),
  UNIQUE INDEX `MateriaaliID_UNIQUE` (`MateriaaliID` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Tilaus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Tilaus` (
  `TilausID` INT NOT NULL AUTO_INCREMENT,
  `TilaajanNimi` VARCHAR(45) NULL,
  `Määrä` INT NULL,
  `MateriaaliID` INT NOT NULL,
  PRIMARY KEY (`TilausID`, `MateriaaliID`),
  UNIQUE INDEX `TilausID_UNIQUE` (`TilausID` ASC) VISIBLE,
  INDEX `fk_Tilaus_Materiaali_idx` (`MateriaaliID` ASC) VISIBLE,
  CONSTRAINT `fk_Tilaus_Materiaali`
    FOREIGN KEY (`MateriaaliID`)
    REFERENCES `mydb`.`Materiaali` (`MateriaaliID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Varaus`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Varaus` (
  `VarausID` INT NOT NULL AUTO_INCREMENT,
  `VaraajanNimi` VARCHAR(45) NULL,
  `MateriaaliID` INT NOT NULL,
  `Määrä` INT NULL,
  PRIMARY KEY (`VarausID`, `MateriaaliID`),
  UNIQUE INDEX `VarausID_UNIQUE` (`VarausID` ASC) VISIBLE,
  INDEX `fk_Varaus_Materiaali1_idx` (`MateriaaliID` ASC) VISIBLE,
  CONSTRAINT `fk_Varaus_Materiaali1`
    FOREIGN KEY (`MateriaaliID`)
    REFERENCES `mydb`.`Materiaali` (`MateriaaliID`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
