CREATE PROC spAM_CarsGetList
	@page INT = 1,
	@rows INT = 10,
	@type VARCHAR(100),
	@brand VARCHAR(100),
	@model VARCHAR(100)
AS
BEGIN

	SELECT c.idCar
			,c.model
			,ct.idCarType
			,ct.carType
			,cb.idCarBrand
			,carBrand
			,TotalRows = COUNT(idCar) OVER()
	FROM tblAM_Cars c WITH(NOLOCK)
	INNER JOIN tblAM_CarsTypes ct WITH(NOLOCK)
		ON c.idCarType = ct.idCarType
	INNER JOIN tblAM_CarsBrands cb WITH(NOLOCK)
		ON c.idCarBrand = cb.idCarBrand
	WHERE ((@type IS NOT NULL AND ct.carType LIKE '%' + @type + '%') OR (@type IS NULL AND 1=1))
		AND ((@brand IS NOT NULL AND cb.carBrand LIKE '%' + @brand + '%') OR (@brand IS NULL AND 1=1))
		AND ((@model IS NOT NULL AND C.model LIKE '%' + @model + '%') OR (@model IS NULL AND 1=1))
	ORDER BY c.model
	OFFSET (@page - 1) * @rows ROWS
	FETCH NEXT @rows ROWS ONLY

END