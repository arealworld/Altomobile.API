CREATE PROC spAM_CarsGetListByType
AS
BEGIN

	SELECT carType = ct.carType
			,totalRows = COUNT(c.idCar)
	FROM tblAM_Cars c
	INNER JOIN tblAM_CarsTypes ct
		ON c.idCarType = ct.idCarType
	GROUP BY c.idCarType,ct.carType

END