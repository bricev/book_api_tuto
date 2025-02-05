terraform {
  required_providers {
    azurerm = {
      source  = "hashicorp/azurerm"
      version = ">= 3.0"
    }
  }
}

provider "azurerm" {
  features {}
}

# Create a Resource Group
resource "azurerm_resource_group" "rg" {
  name     = "rg-bookapi"
  location = "francecentral" # Adjust to your preferred Azure region
}

# Create an Azure Container Instance (ACI)
resource "azurerm_container_group" "bookapi" {
  name                = "bookapi-container"
  location            = azurerm_resource_group.rg.location
  resource_group_name = azurerm_resource_group.rg.name
  os_type             = "Linux"

  # Use a public IP and assign a DNS label so that your container is reachable from the internet
  ip_address_type = "Public"
  dns_name_label  = "bookapitutorialbv" # must be globally unique within Azure DNS

  container {
    name   = "bookapi"
    image  = "your-dockerhub-account/bookapi:latest" # Change to your container image reference
    cpu    = 0.5
    memory = 1.5

    ports {
      port     = 80
      protocol = "TCP"
    }

    # Set the environment variable so your app binds to all network interfaces
    environment_variables = {
      ASPNETCORE_URLS = "http://+:80"
    }
  }

  tags = {
    environment = "production"
  }
}
