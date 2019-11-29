﻿/* Post creation script here. Seed data here*/

INSERT INTO dbo.AspNetRoles (Id, Name, Discriminator) VALUES ('252af317-07fb-4021-b33f-156c19ab258b', 'Client', 'ApplicationRole'),
		('6f21fe44-08aa-4f1c-be97-10ea6a065b29', 'Admin', 'ApplicationRole'), 
		('70ab5a67-c1cc-42f6-be13-d9460accd661', 'SuperAdmin', 'ApplicationRole')

/* 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==' is 'Qwerty1234!' hashed */

INSERT INTO dbo.AspNetUsers (Id, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumber, PhoneNumberConfirmed,  TwoFactorEnabled, LockoutEndDateUtc, LockoutEnabled, AccessFailedCount, UserName)
VALUES  ('85846eae-d124-43b0-8046-5a18815741c8', 'admin@admin.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==', '69c4e121-ffc4-4c0d-9c6c-c679653c0158', NULL, 0, 0, NULL, 0, 0, 'admin@admin.com'),
		('1ff6e90b-ec17-4160-81c8-749cc837a0ef', 'client_1@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_1@client.com'),
		('2ff6e90b-ec18-4160-81c8-749cc837a0ef', 'client_2@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_2@client.com'),
		('3ff6e90b-ec19-4160-81c8-749cc837a0ef', 'client_3@client.com', 1, 'AAcarx9c3rzdkFOz0cceNihKBsouXqd0uEspt0BuZuuyWj3ODVmhT6lRWXwEbRTasg==','c67cb9a7-7d8b-4dec-9f67-6b93fa2de3b5', NULL, 0, 0, NULL, 0, 0, 'client_3@client.com')


INSERT INTO dbo.AspNetUserRoles (UserId, RoleId)
VALUES	('85846eae-d124-43b0-8046-5a18815741c8', '6f21fe44-08aa-4f1c-be97-10ea6a065b29'),
		('1ff6e90b-ec17-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b'), 
		('2ff6e90b-ec18-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b'),
		('3ff6e90b-ec19-4160-81c8-749cc837a0ef', '252af317-07fb-4021-b33f-156c19ab258b')
