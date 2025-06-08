Mettre la base de données à jour:
  -  Se placer dans le dossier data et lancer la commande: dotnet ef database update --startup-project ../ArtMarketplace
    
Lancement de l'application;
  -  Se placer dans le dossier ArtMarketPlace (celui qui contient le fichier Program.cs) et lancer la commande: "dotnet run" ou "dotnet run -debug" pour la lancer en debug et avoir accès au swagger  

Cas d'usage implémentés:

  Utilisateur non identifié:
  -  Inscription: Un nouvel utilisateur peut s'inscrire en décidant de son rôle 
  -  Connection: Permet à un utilisateur inscrit de se connecter en tant que soit Artisan, Client ou partenaire de livraison

  Utilisateur identifié en tant qu'artiste
  -  Ajouter un produit: Via l'onglet ajouter un produit dans la nav bar
  -  Voire la liste de ses produit: sur l'écran d'accueil une fois qu'on s'est identifié
  -  Filtrer en fonction du statut: Disponible, réservé ou acheter (Réserver signifie que le produit est dans le panier d'un client)
  -  Voire ses gains: Sur le même écran en haut à droite on vois les gains actuels de l'artiste
  -  Modifier un produit: dans  la liste de ses produit l'artiste peut modifier un produit si celui-ci est toujours dans l'état disponible
  -  Voire l'avis du client qui a acheté un produit: pour un produit acheté et pour lequel le client à mis un avis l'artiste peut le consulter
  -  Ecrire une réponse à un avis: sur l'écran de consultation d'un avis l'artiste peux aussi y répondre
  -  Signifier qu'un produit à été récupéré chez lui par le livreur
  -  Se déconnecter via la nav bar
    
  Utilisateur identifié en tant que client
  -  Voire la liste des produits disponible: sur l'écran d'accueuil
  -  Ajouter un produit à son panier
  -  Consulter le contenu de son panier: accessible via la nav-bar 
  -  Confirmer et acheter le contenu du panier: via le bouton en bas à droite de l'écran
  -  Consulter les produits achetés: via la nav-bar
  -  Filtrer les produits acheté en fonction de si ils sont en livraison ou déja livré
  -  Voire l'état d'avancement de la livraison des produit en liraison
  -  Laisser un avis sur un produit
  -  Modifier cet avis
  -  Voire la réponse éventuelle de l'artiste à son avis
  -  Se déconnecter via la nav bar

  Utilisateur connecté en tant que partenaire de livraison
  -  Le partenaire de livraison a accès à l'ensemble des produit qu'il doit livrer
  -  Il a la possibilité de passer l'état de livraison à l'étape suivante (Excepté le fait qu'il a récupéré le colis chez l'artiste qui doit être validé par l'artiste)
  -  Se déconnecter via la nav bar
  
