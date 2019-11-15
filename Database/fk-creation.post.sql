/*
Modèle de script de post-déploiement							
--------------------------------------------------------------------------------------
 Ce fichier contient des instructions SQL qui seront ajoutées au script de compilation.		
 Utilisez la syntaxe SQLCMD pour inclure un fichier dans le script de post-déploiement.			
 Exemple :      :r .\monfichier.sql								
 Utilisez la syntaxe SQLCMD pour référencer une variable dans le script de post-déploiement.		
 Exemple :      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/

ALTER TABLE [dbo].[command] ADD CONSTRAINT command_machine_id_fk FOREIGN KEY (machine_id) REFERENCES [dbo].[machine] (id);
ALTER TABLE [dbo].[command] ADD CONSTRAINT command_client_id_fk FOREIGN KEY (client_id) REFERENCES [dbo].[client] (id);
ALTER TABLE [dbo].[bill] ADD CONSTRAINT bill_client_id_fk FOREIGN KEY (client_id) REFERENCES [dbo].[client] (id)
