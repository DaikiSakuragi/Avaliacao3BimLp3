using Microsoft.Data.Sqlite;
using Avaliacao3BimLp3.Database;
using Avaliacao3BimLp3.Repositories;
using Avaliacao3BimLp3.Models;

var databaseConfig = new DatabaseConfig();

var databaseSetup = new DatabaseSetup(databaseConfig);

var productRepository = new ProductRepository(databaseConfig);

var modelName = args[0];
var modelAction = args[1];

if (modelName == "Product")
{
     if (modelAction == "New")
    {
        
        int id = Convert.ToInt32(args[2]);
        string name = args[3];
        double price = Convert.ToDouble(args[4]);
        bool active = Convert.ToBoolean(args[5]);
        
        if(productRepository.ExistsById(id))
        {
            System.Console.WriteLine($"Produto com Id {id} já existe");
        }
        else
        {
            var product = new Product(id, name, price, active);
            productRepository.Save(product);
            System.Console.WriteLine($"Produto {name} cadastrado com sucesso");
        }
    }

    if (modelAction == "List")
    {
        var lista = productRepository.GetAll();
        if(lista.Any())
        {
            foreach (var product in lista)
            {
                System.Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}"); 
            }
        }
        else
        {
            System.Console.WriteLine("Nenhum produto cadastrado");
        }
        
    }

     if(modelAction == "Delete")
    {
        
        int id = Convert.ToInt32(args[2]);
        if(productRepository.ExistsById(id))
        {
            productRepository.Delete(id);
            System.Console.WriteLine($"Produto {id} removido com sucesso");
        }
        else
        {
            Console.WriteLine($"Produto {id} não encontrado.");
        }        
    }

    if(modelAction == "Enable")
    {
        var id = Convert.ToInt32(args[2]);

        if (!productRepository.ExistsById(id))
        {
            Console.WriteLine($"Produto {id} não encontrado");
        }

        else
        {
            productRepository.Enable(id);
            Console.WriteLine($"Produto {id} habilitado com sucesso");
        }
    }

    if(modelAction == "Disable")
    {
        var id = Convert.ToInt32(args[2]);

        if (!productRepository.ExistsById(id))
        {
            Console.WriteLine($"Produto {id} não encontrado");
        }

        else
        {
            productRepository.Disable(id);
            Console.WriteLine($"Produto {id} desabilitado com sucesso");
        }

    }

    if (modelAction == "PriceBetween")
    {
        double initialPrice = Convert.ToDouble(args[2]);
        double endPrice = Convert.ToDouble(args[3]);

        var lista = productRepository.GetAllWithPriceBetween(initialPrice, endPrice);

        if(lista.Any())
        {
            foreach (var product in lista)
            {
                System.Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}"); 
            }
        }
        else
        {
            System.Console.WriteLine("Nenhum produto cadastrado");
        }
        
    }

    if (modelAction == "PriceHigherThan")
    {
        double price = Convert.ToDouble(args[2]);

        var lista = productRepository.GetAllWithPriceHigherThan(price);
        if(lista.Any())
        {
            foreach (var product in lista)
            {
                System.Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}"); 
            }
        }
        else
        {
            System.Console.WriteLine("Nenhum produto cadastrado");
        }
        
    }

    if (modelAction == "PriceLowerThan")
    {
        double price = Convert.ToDouble(args[2]);
        var lista = productRepository.GetAllWithPriceLowerThan(price);
        if(lista.Any())
        {
            foreach (var product in lista)
            {

            System.Console.WriteLine($"{product.Id}, {product.Name}, {product.Price}, {product.Active}"); 
            }
        }
        else
        {
            System.Console.WriteLine("Nenhum produto cadastrado");
        }
        
    }

    if(modelAction == "AveragePrice")
    {
        double media = productRepository.GetAveragePrice();

        if(media == 0)
        {
            System.Console.WriteLine("Nenhum produto cadastrado");
        }
        else
        {
            System.Console.WriteLine("A média dos preços é: R$" + media);
        }
        
    }
}
