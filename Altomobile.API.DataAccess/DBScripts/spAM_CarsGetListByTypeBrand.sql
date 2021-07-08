CREATE PROC spAM_CarsGetListByTypeBrand
AS
BEGIN

	SELECT carType = ct.carType
			,carBrand = cb.carBrand
			,totalRows = COUNT(*)
	FROM tblAM_Cars c
	INNER JOIN tblAM_CarsTypes ct
		ON c.idCarType = ct.idCarType
	INNER JOIN tblAM_CarsBrands cb
		ON c.idCarBrand = cb.idCarBrand
	GROUP BY c.idCarType,ct.carType,c.idCarBrand,cb.carBrand

END