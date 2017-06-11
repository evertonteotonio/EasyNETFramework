EasyNETFramework
----------------

The framework for .NET developers to develop mobile applications and websites using the same code repository.

So this is you trying to create a website and mobile application all at once:

![](https://fat.gfycat.com/OrangeVastAntarcticgiantpetrel.gif)

You need to work in two separate projects and completely rewrite your logic to match mobile experience in a world that is going very fast and you need to be in market in no time. So drop these xamarin (no offense) books and let's get started creating a website and a mobile application all at once!

----------

## EasyNETFramework Structure ##

The EasyNETFramework is built on ASP.NET Core 1.1, [`Dapper`](https://github.com/StackExchange/Dapper) as an ORM, and [`VueJS`](https://vuejs.org/). The UI part is handled using [`Quasar Framework`](http://quasar-framework.org/) which is built using VueJS and it has many features already embedded on it that allow to create the UI, for different platforms. It supports websites, desktop (using electron), mobile apps (using cordova). 

The solution:

![The soultion projects](https://raw.githubusercontent.com/amr-swalha/EasyNETFramework/master/Documentation/Images/1-Structure.png)

 - Common
	 - Where common code are placed, and where we place our configuration like connectionstrings, etc. (For sure you were expecting a app.config or XML config file. You will have them repeated throughout the project and you will need to make sure these are consistent so instead of duplicate files we use the common project)
 - Entity
	 - Where we place our POCO classes
 - Data
	 - Where we create a DbSet like for our entity and also where we can add a custom data access methods that are not available in the framework .
 - RESTFul
	 - Where we send requests to receive and save data.
 - UI
	 - Where we create our web pages, and what the user will see. In VueJS using the Quasar Framework.

So enough chit-chat let's get started developing things! Make sure you check the wiki for full reference, this is a quick getting started.

## Getting started with EasyNETFramework
Clone or download this repository to follow along.

----------
Before Starting:

 - Change the connection string in the common project. Also, run the sampledb script on your desired database.
 - To install the needed NodeJS for the UI project, open the commad line in the UI project folder and type `npm install -g quasar-cli` and then `npm install`. To run the UI project use `quasar dev` command in the command line.


----------


 1. Adding a new entity
Go to your database and add a table with whatever columns you need, for this tutorial we will add an `Order` table and then add the POCO class to the entity project as follow (Make sure you have the Key attribute over the Id column and make sure it's nullable):

![SQL Table](https://raw.githubusercontent.com/amr-swalha/EasyNETFramework/master/Documentation/Images/2-Order.png)


![POCO Entity](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/3-Order%20entity.png?raw=true)

Then you will need to add an GeneralManager field so you can access that entity and save and retrieve data. Go to the Data project, and in the Context class add the filed of type Order:

![DbSet Like in the Context](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/4-Order%20context.png?raw=true)

Now, we will go to RESTFul project and add a Web API Controller that inherit the BaseController (You can add a new Web API controller from the Add New File but just remember to change it to inherit from the base Controller):

![API Controller](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/5-Order%20API%20controller.png?raw=true)

Now just simply call the DbContext and call whatever method you like to perform the operation you desire. For now we will use the FindAll as follow (The search object is a not mapped entity and used to pass different query strings like limiting number of rows, paging, etc.):

![FindAll](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/6-Order%20API%20FindAll.png?raw=true)

Now let's go to the Quasar Side and add a new VueJS Component in the src/components folder in the UI project. We will add a new folder and call it order and place inside it an index.vue file:

![Component](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/7-Order%20vue.png?raw=true)


And it will contain the following code:


    <template>
      <div>
        <h3>Orders</h3>
        <table class="q-table">
          <thead>
            <tr>
              <th></th>
              <th class="text-left">Order Title</th>
              <th class="text-right">Amount</th>
              <th class="text-right">Customer Name</th>
            </tr>
          </thead>
          <tbody>
            <tr v-for="item in orders">
              <td><input type="checkbox" value="" @click="changeState(item)"/></td>
              <td class="text-left">{{item.orderTitle}}</td>
              <td class="text-right">${{item.amount}}</td>
              <td class="text-right">{{item.customerName}}</td>
            </tr>
          </tbody>
        </table>
      </div>
    </template>
    <script>
    export default {
      data () {
        return {
          orders: null
        }
      },
      methods: {
        // API Calls
        getItems: function () {
          this.$http.get('Order/FindAll').then(response => {
            // get body data
            this.orders = response.body
          }, response => {
            // error callback
          })
        },
        changeState: function (item) {
          console.log(item)
          item.isSelected = !item.isSelected
        }
      },
      created () {
        // When first the component load we get data 
        this.getItems()
      }
    }
    </script>
    <style lang="styl">
    
    </style>



Now we will need to add a new route definitions in the router.js file so we map the URL to the component we created:

![Router definitions ](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/8-Order%20router.png?raw=true)

Final Step (I know you got bored already!) We just need to add the Order to the navigation side menu and we are all set (In the Index.vue file in the root components folder):

![Navigation link](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/9-Order%20menu.png?raw=true)

Now, first you need to run Quasar by typing quasar dev in the command line. After that run the Restful Project and login with the default admin account (admin, 123456) and make sure you have some dummy data in the database and here you go!

![Finally!](https://github.com/amr-swalha/EasyNETFramework/blob/master/Documentation/Images/10-Order%20result.png?raw=true)