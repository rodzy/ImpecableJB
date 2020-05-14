![GitHub stars](https://img.shields.io/github/stars/rodzy/ImpecableJB?style=social)
![GitHub last commit](https://img.shields.io/github/last-commit/rodzy/ImpecableJB)
# ImpecableJB
An ASP.Net C# e-comerce based app using MVC entity framework and Bootstrap.<br/>
The application is associated with an SQL database, related to the sale of cleaning products and
services in which you need to make purchases or orders. 🛒 <br/>
*P.S: This project was part of one my ASP.NET courses in college*

## Modules

This modules are the basic tasks, but in reality the Models & Controller layer in the project are way more complex to handle all the relations between them.<br/>
*P.S: It's safe to mention that the code was written in Spanish as a requirement for my course*

- [Users](https://github.com/rodzy/ImpecableJB#users)
- [Products](https://github.com/rodzy/ImpecableJB#products)
- [Purchase & details](https://github.com/rodzy/ImpecableJB#purchase-&-details)
- [Ranking system](https://github.com/rodzy/ImpecableJB#ranking-system)
- [Coupons](https://github.com/rodzy/ImpecableJB#coupons)

## Users
Management for user type: Client / Administrator
- Client: <br/>
    - Register, edit, delete and display information.
    - Login phase with email and password requirement.
    - Update password.
    - Can review all his purchases and coupons. <br/>
- Administrator: <br/>
    - Can access to all functions except from sensitive user information, like passwords.<br/>
    
Used Session property to display user information in the layout
    
## Products

- List of products displayed all by default.
- Used JQuery Ajax to filter by product type to make the selection easier.
- Can add products to cart from the main page and display their description.

## Purchase & details

- An order can be registered without being authenticated.
- The order will not got through if the user is not authenticated.
- The purshase will be on hold until you press the yellow button that will take care of pricing and discounts.
- Details information will be available on the user profile.

## Ranking system

- Each new user will start with an Unranked placement.
- The ranks will change depending on past purchases.(*Used colones(₡) Costa Rica currency*)
- Ranks:<br/>
    - Unraked: Starter rank
    - Bronze: ₡10.000 +
    - Silver: ₡20.000 +
    - Gold: ₡40.000 +
    - Diamond: ₡60.000 +
    
## Coupons
Coupons represent discounts or royalties, the higher the level, the higher the customer compensation.

- Register:
   - Coupons are registered by the administrator.
   - Each coupon is associated with a product and a customer level, this way we can control if the product that is on the cart is elegible for a discount.
   - The administrator can assign one use coupons for each user, just by typing his email(*If the user's rank is diferent from the coupon rank, the coupon cannot be assinged*).

- Details:
   - Each user can look at his available coupons and can interact with them.
   - Coupons can be used only if the coupon item is on the shopping cart.

- Coupon exchange:
   - The administrator is responsible for registering coupon redemption.
   - Once the exchange is registered, the coupon does not appear as available to the client.
   - The client can see a detail of the exchange record.
   
## License
[MIT](https://choosealicense.com/licenses/mit/)
