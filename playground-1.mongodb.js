use("BTL_MONGO");

// Create a new collection.
db.createCollection("Categories");
db.Categories.insertMany([
    { _id: 1, CategoryName: "Digestive medicine" },
    { _id: 2, CategoryName: "Headache and pain reliever" },
    { _id: 3, CategoryName: "Fever and flu medicine" },
    { _id: 4, CategoryName: "Microscope" },
    { _id: 5, CategoryName: "Blood pressure monitors" },
    { _id: 6, CategoryName: "Cardiac event monitor" },
    { _id: 7, CategoryName: "Pulse oximeter" },
    { _id: 8, CategoryName: "Medical Exam tables" }
])

db.createCollection("Products");
db.Products.insertMany([
    { _id: "B01", ProductName: "Culturelle", Price: 1000, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "Culturelle.jpg", ProductStatus: "In stock", CategoryId: 1 },
    { _id: "B02", ProductName: "Gas-X Relief Tablets", Price: 1100, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "GasXReliefTablets.jpg", ProductStatus: "In stock", CategoryId: 1 },

    { _id: "B03", ProductName: "Advil Liqui-Gels", Price: 2000, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "AdvilLiquiGels.jpg", ProductStatus: "In stock", CategoryId: 2 },
    { _id: "B04", ProductName: "Motrin IB", Price: 1500, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "MotrinIB.jpg", ProductStatus: "In stock", CategoryId: 2 },

    { _id: "B05", ProductName: "Boiron Oscillococcinum", Price: 2000, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "BoironOscillococcinum.jpg", ProductStatus: "In stock", CategoryId: 3 },

    { _id: "B06", ProductName: "AmScope 120X-1200X 52-pcs", Price: 900, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "AmScope_120X_1200X.jpg", ProductStatus: "In stock", CategoryId: 4 },

    { _id: "B07", ProductName: "Portable-Small Blood-Pressure Monitors", Price: 2200, Description: "Brand Culturelle Flavor	Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "PortableSmall.jpg", ProductStatus: "In stock", CategoryId: 5 },

    { _id: "B08", ProductName: "Zacurate 500C Elite", Price: 4000, Description: "Zacurate 500C Elite Unflavored Primary Supplement Type	Probiotic Unit Count	30 Count Item Form	Capsule Item Weight	4.54 Grams Item Dimensions LxWxH	1.25 x 3.66 x 5.5 inches Special Ingredients	All Natural Diet Type	Gluten Free Product Benefits Digestive Health Support, Immune Support", ProductImage: "Zacurate_500CElite.jpg", ProductStatus: "In stock", CategoryId: 7 }

])

db.createCollection("Account");
db.Account.insertMany([
    { _id: 1, FullName: "Admin", Email: "admin@gmail.com", Password: "admin", Phone: "0912345678", Address: "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội", Avatar: "acc2.jpg", AccountStatus: "Disable", AccountType: "0" },
    { _id: 2, FullName: "User", Email: "user@gmail.com", Password: "User", Phone: "0912345678", Address: "Tòa Nhà HTC-250 Hoàng Quốc Việt-Cổ Nhuế-Cầu Giấy-Hà Nội", Avatar: "acc2.jpg", AccountStatus: "Disable", AccountType: "1" }
])

use("BTL_MONGO");
db.createCollection("Cart");
db.Cart.insertMany([
    { _id: 1, Quantity: 1, TotalPrice: 1000, ProductId: "B01", AccountId: 1 },
    { _id: 2, Quantity: 1, TotalPrice: 1000, ProductId: "B01", AccountId: 2 }
])
db.createCollection("Orders");
db.Orders.insertMany([
    { _id: 1, ReceiverName: "OOOO", ReceiverPhone: "ooooo", ReceiverAddress: "ooooo", Note: "ooooo", OrderDate: "04/01/2003", OrderStatus: "oooooo", AccountId: 1 },
    { _id: 2, ReceiverName: "OOOO", ReceiverPhone: "ooooo", ReceiverAddress: "ooooo", Note: "ooooo", OrderDate: "04/01/2003", OrderStatus: "ooooo", AccountId: 1 }
])
db.createCollection("OrderDetail");
db.OrderDetail.insertMany([
    { _id: 1, Quantity: 1, TotalPrice: 10, ProductId: "1", OrdersId: 1 },
    { _id: 2, Quantity: 1, TotalPrice: 10, ProductId: "1", OrdersId: 1 }
])



use("BTL_MONGO");
db.Products.aggregate([
    {
        $lookup: {
            from: "Categories",
            localField: "CategoryId",
            foreignField: "_id",
            as: "Categories"
        }
    }
]).pretty()

use("BTL_MONGO");
db.Cart.aggregate([
    {
        $lookup: {
            from: "Products",
            localField: "ProductId",
            foreignField: "_id",
            as: "Products"
        }
    },
    {
        $lookup: {
            from: "Account",
            localField: "AccountId",
            foreignField: "_id",
            as: "Account"
        }
    }
]).pretty()

use("BTL_MONGO");
db.Orders.aggregate([
    {
        $lookup: {
            from: "Account",
            localField: "AccountId",
            foreignField: "_id",
            as: "Account"
        }
    }
]).pretty()

use("BTL_MONGO");
db.OrderDetail.aggregate([
    {
        $lookup: {
            from: "Products",
            localField: "ProductId",
            foreignField: "_id",
            as: "Products"
        }
    },
    {
        $lookup: {
            from: "Orders",
            localField: "OrdersId",
            foreignField: "_id",
            as: "Orders"
        }
    }
]).pretty()