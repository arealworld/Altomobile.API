INSERT INTO tblAM_CarsTypes(carType) VALUES('small')
DECLARE @idSmall INT
SET @idSmall = @@IDENTITY

INSERT INTO tblAM_CarsTypes(carType) VALUES('medium')
DECLARE @idMedium INT
SET @idMedium = @@IDENTITY

INSERT INTO tblAM_CarsTypes(carType) VALUES('large')
DECLARE @idLarge INT
SET @idLarge = @@IDENTITY

--------------------------------------------------------------

INSERT INTO tblAM_CarsBrands(carBrand) VALUES('BMW')
DECLARE @idBMW INT
SET @idBMW = @@IDENTITY

INSERT INTO tblAM_CarsBrands(carBrand) VALUES('AUDI')
DECLARE @idAUDI INT
SET @idAUDI = @@IDENTITY

INSERT INTO tblAM_CarsBrands(carBrand) VALUES('JEEP')
DECLARE @idJEEP INT
SET @idJEEP = @@IDENTITY

--------------------------------------------------------------

INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('X6',@idLarge,@idBMW)

INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('A1',@idSmall,@idAUDI)
INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('A3',@idMedium,@idAUDI)

INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('RENEGADE',@idMedium,@idJEEP)
INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('WRANGLER',@idLarge,@idJEEP)
INSERT INTO tblAM_Cars(model,idCarType,idCarBrand) VALUES('CHEROKEE',@idLarge,@idJEEP)