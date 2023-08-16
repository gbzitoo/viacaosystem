#!/bin/bash

apiappname="apiviacao-apim"

printf "Setting username and password for Git ... (1/7)\n\n"


GIT_USERNAME="Gabriel Oliveira AZURE"
GIT_EMAIL="gabrieloliveira851@hotmail.com"

git config --global user.name "$GIT_USERNAME"
git config --global user.email "$GIT_EMAIL"


RESOURCE_GROUP=$(az group list --query "[0].name" -o tsv)

# Create App Service plan
PLAN_NAME=aviacaoResource


printf "\nCreating App Service plan in FREE tier ... (2/7)\n\n"


az appservice plan create --name $apiappname --resource-group $RESOURCE_GROUP --sku FREE --location centralus --verbose

printf "\nCreating API App ... (3/7)\n\n"

az webapp create --name $apiappname --resource-group $RESOURCE_GROUP --plan $apiappname --deployment-local-git --verbose


printf "\nSetting the account-level deployment credentials ...(4/7)\n\n"


DEPLOY_USER="gabrieloliveira851@hotmail.com"
DEPLOY_PASSWORD="@Gabri8511"

az webapp deployment user set --user-name $DEPLOY_USER --password $DEPLOY_PASSWORD --verbose


GIT_URL="https://$DEPLOY_USER@$apiappname.scm.azurewebsites.net/$apiappname.git"

# Create Web App with local-git deploy

REMOTE_NAME=production


# Set remote on src
printf "\nSetting Git remote...(5/7)\n\n"


git remote add $REMOTE_NAME $GIT_URL


printf "\nGit add...(6/7)\n\n"

git add .
git commit -m "initial revision"


printf "\nGit push... (7/7)\n\n"


# printf "When prompted for a password enter this: $DEPLOY_PASSWORD\n"
# git push --set-upstream $REMOTE_NAME master
git push "https://$DEPLOY_USER:$DEPLOY_PASSWORD@$apiappname.scm.azurewebsites.net/$apiappname.git"


printf "Setup complete!\n\n"

printf "***********************    IMPORTANT INFO  *********************\n\n"


printf "Swagger URL: https://$apiappname.azurewebsites.net/swagger\n"


printf "Swagger JSON URL: https://$apiappname.azurewebsites.net/swagger/v1/swagger.json\n\n"

