Emotweecon
-------------------------

Proof of Concept for a messaging app where messages are thrown at Sentiment Analysis (api at http://sociallytic.dk) and the messages are being assigned an appropriate smiley, matching the general feeling of that "Tweet".

Live at http://emotweecon.azurewebsites.net

Setup
-------------------------

In order to make this work you must

 1. Create an Azure Storage Account and copy the connectionstring into the app.config and web.config files
 2. Change the webroot setting in Webjob App.config in order to tell SignalR where to send notifications
 3. Publish the website to an Azure Website
 4. Publish the webjob to the same website
 5. Or just hook up the azure website to bitbucket or github. Deployment is way easier that way.
 6. Remember to copy connectionstring into AzureWebJobsDashboard connstring in AzurePortal)
 