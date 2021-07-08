# AltoMobile.API
### _Technical challenge, backend developer opportunity_

![](https://drive.google.com/uc?export=view&id=1ck9SsqNfpsQuuA1oGkBLtVrnmYJhDTiW)

### Objetive
The goal is to create a RESTful API to handle a simple car rental flow, where the endpoints are secured with an authorization method and includes a reporting system.

- **1.** The API has to be protected with a security mechanism. It needs to return the appropriate error code for “not authorized”.
![](https://drive.google.com/uc?export=view&id=1jlFK0So6adlV6cEvmvCiAPz8FfxYgSRY)
![](https://drive.google.com/uc?export=view&id=1aQlCZmNn5BBWOHAtWnLQLOu0Ts09unWK)
- **2.** In case of incorrect parameters or request body, it has to return the right code for “bad request”.
![](https://drive.google.com/uc?export=view&id=1FORw1YavjkKgslA7L9lnWgqWjEvv5x-V)
- **3. Login endpoint**: it authenticates an account and returns the needed information to access the other endpoints.
  - a. Body: user: string / password: string
  - b. Response: needed info to authorization the others endpoints
![](https://drive.google.com/uc?export=view&id=17zXvBQNJt058G6YrP-JNZQjf7tm46enu)
- **4. Search cars endpoint**: we want to know the availability of cars, searched by type, brand or model. For the “type” parameter, there are only 3 valid values.
  - a. Body:
    - i. type: {small, medium, large}
    - ii. brand: string
    - iii. model: string
  - b. Response: List of cars
![](https://drive.google.com/uc?export=view&id=15ucocTMrUi4W4E9CgDPgbOYxJzNZDfdf)
- **5. Reporting endpoint**: implement an algorithm that returns a report of the cars rented until now receiving 1 to 2 parameters for grouping.
  - a. Examples:
    - i. With the parameter “type”: { small: 5, medium: 6, big: 2 }
    - ii. With the parameter “type” and “brand”: { small: { nissan: 3, chevrolet: 2 }, medium: { mercedes: 5, toyota: 1 }, big: { tesla: 2 } }
    - iii. With the parameter “brand” and “type”: { nissan: { small: 1, medium: 2 } }
![](https://drive.google.com/uc?export=view&id=18C2f5LN925zahnIWcjHeKV7etE57AUNW)
- **6.** The data can be dummy info and it is possible to use a real DB (the DB creation script should be provided) or an in-memory database.
- **7.** Provide valid user/pass for testing.
- **8.** Upload the project to a git repository.
- **9.** Add instructions to run the project.
- **10.** Extra: set up a swagger (or an insomnia.rest) to make testing easier.

### Summary

The application was built with .NET Core version 3.1, it contains four main projects:

- **Altomobile.API.Domain**, contains only classes related to business entities.
![](https://drive.google.com/uc?export=view&id=1mRydT9BaMeFVXJc1FQ70YI1NQbyIgR1a)
- **Altomobile.API.DataAccess**, contains the methods that connect to the database. It uses the **Autofac** library to implement the **inversion of control design pattern** (it is contained into Container folder). **Dapper** library is used as **ORM**. This layer contains the scripts for all objects which were created on the database.
![](https://drive.google.com/uc?export=view&id=1IQfLc2_Ppn8tj2s8Qt7y2ht5uVFiEza6)

    The **ICommon** interface uses **generics** to reuse data access methods:
    ![](https://drive.google.com/uc?export=view&id=1Dgb5MqCNn0fKIKI2vRjKL6a_w2wLUFMf)    
- **Altomobile.API.BusinessLogic**, contains the implementation for each business entity to call the data access layer and return values to the presentation layer. Using **Autofac** ensures very easy scalability to the application.
![](https://drive.google.com/uc?export=view&id=1NYcRZ9ZfeBqqHmUn1z_nmYI22mdyjYXN)
- **Altomobile.API.UI**, is the **presentation layer** of the services. **This implements security to expose the services**, the user must first obtain a **session token (JWT)** and send it later in the request header to consume the protected services. This implements **Swagger** to self-document the service layer and integrate a rest client to facilitate testing.
![](https://drive.google.com/uc?export=view&id=1wUrb3b25p7Jf4KQeEMUeu0tC8sjQH51Z)

### How to use

`Note: The database is in a free Azure account, so sometimes the first call is not successful because the database service is not available. The application returns the error message corresponding to this problem. After a brief moment, it is possible to consume the service that you wanted to use again.`

You just need to **clone the project, open the solution and press run**, then the visual studio editor will automatically open the browser with the main page of the project pointing to the swagger client where you can directly test each service.

**1. Session token**. The first thing is to authenticate with the service "Authenticate" to obtain a session token. The authentication data are the following:
- _User_: charly.brown@outlook.com
- _Password_: **276977568b01a8bdc021d3784a1d7c2c**

![](https://drive.google.com/uc?export=view&id=1bFohfVF9MUADypBXGzUdb-5PJgmLsgCl)
![](https://drive.google.com/uc?export=view&id=1OZnfHKEuTD8_Lkvg3QbtN-xTUj7eiiS5)
The service must respond to the session token, which is the entire long chain that must be sent in each request. **This value must be copied for later use**.
![](https://drive.google.com/uc?export=view&id=1cUI85SEVJgOjnal7qPnKZwWj7EN4-bsD)

**2. Authentication**. To authenticate globally in the Swagger client we must do the following:
![](https://drive.google.com/uc?export=view&id=1AS1aRxyfjS8M0UL0PHGuR19NEm8m6ZmC)
In the value of the field you must write the word **bearer** + **session token**:

![](https://drive.google.com/uc?export=view&id=1pwTW3q4huCLQLFlFxyFIwO5GWNAUTLlA)

**Ready! Now you can use the protected services**.

![](https://drive.google.com/uc?export=view&id=1foB_j49M4UOrwJvwmDAPE04M8dlL8MtY)

**3. Search cars endpoint**. It is a **paginated** search service, the search parameters are described below:
- **page**: The page of the data you want to consult.
- **rows**: The total of rows you want to consult per page.
- **type**: Search through the type name of the vehicle (small, medium, large).
- **brand**: Search through the brand name of the vehicle.
- **model**: Search through the model name of the vehicle.

![](https://drive.google.com/uc?export=view&id=15DHSFAzU3ncevkhdQyWT2iVQawaPYQpA)
![](https://drive.google.com/uc?export=view&id=1DIonhtuftdsBd56b60KZhcf-APsOCNBF)
![](https://drive.google.com/uc?export=view&id=1v_YzcPer-t9Vdzo2CF-7oeCyldwjIwIp)

**4. Reporting endpoint**. Returns a report grouped of the cars. Available values to get report:
- 1 = By Type
- 2 = By Type and Brand
- 3 = By Brand and Type)

![](https://drive.google.com/uc?export=view&id=1GjVlWvgWQaz4qEBzDZL4ORX6pZiIOVeN)
![](https://drive.google.com/uc?export=view&id=1jNxn6WtLeTLuG5F26T78-_8JcrmxnFEO)
![](https://drive.google.com/uc?export=view&id=1xT0FUmWaudh5JYsKQvNvAdZNypM_R-Df)
