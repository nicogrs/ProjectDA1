INSERT INTO dbo.[__EFMigrationsHistory] (MigrationId,ProductVersion) VALUES
	 (N'20241119045340_PrimeraMigracion',N'7.0.9');
INSERT INTO dbo.Trashes (ElementsCount) VALUES
	 (0),
	 (0),
	 (0),
	 (0),
	 (0),
	 (0),
	 (1);
INSERT INTO dbo.Users (Name,Surname,Email,BirthDate,Password,Admin,PaperBinId) VALUES
	 (N'Super',N'Admin',N'admin@taskpanel.com','2000-08-30 00:00:00.0000000',N'Admin123$',1,1),
	 (N'Josefa',N'Gomez',N'josefa@taskpanel.com','1999-09-13 00:00:00.0000000',N'Gomez123$',0,2),
	 (N'Ainoa',N'Gordo',N'ainoa@taskpanel.com','2001-10-09 00:00:00.0000000',N'Gordo123$',1,3),
	 (N'Roger',N'Jaramillo',N'roger@taskpanel.com','2005-01-04 00:00:00.0000000',N'Jaramillo123$',0,4),
	 (N'Leo',N'Florez',N'leo@taskpanel.com','1988-07-12 00:00:00.0000000',N'Florez123$',0,5),
	 (N'Remedios',N'Gordillo',N'remedios@taskpanel.com','1930-06-07 00:00:00.0000000',N'Gordillo123$',0,6),
	 (N'Aitana',N'Tejera',N'aitana@taskpanel.com','1999-05-10 00:00:00.0000000',N'Tejera123$',0,7);

INSERT INTO dbo.Teams (Name,CreatedOn,TasksDescription,MaxUsers,MembersCount) VALUES
	 (N'Equipo 1','2024-11-20 07:34:39.2216000',N'Descripcion Tareas 1',5,3),
	 (N'Equipo 2','2024-11-20 07:53:13.0578388',N'Descripcion Tareas 2',10,2),
	 (N'Equipo 3','2024-11-20 07:53:34.4673567',N'Descripcion Tareas 3',8,1),
	 (N'Equipo 4','2024-11-20 07:53:43.6640363',N'Descripcion Tareas 4',4,1),
	 (N'Equipo 5','2024-11-20 07:54:10.7218282',N'Descripcion Tareas 5',2,1),
	 (N'Equipo 6','2024-11-20 07:54:37.4755462',N'Descripcion Tareas 6',6,1),
	 (N'Equipo 7','2024-11-20 07:54:57.1936385',N'Descripcion Tareas 7',3,1),
	 (N'Equipo 8','2024-11-20 07:55:06.3843086',N'Descripcion Tareas 8',9,1),
	 (N'Equipo 9','2024-11-20 07:55:13.8105878',N'Descripcion Tareas 9',9,1),
	 (N'Equipo 10','2024-11-20 07:55:28.7410280',N'Descripcion Tareas 10',7,1);
INSERT INTO dbo.TeamUser (TeamMembersId,UserTeamsId) VALUES
	 (1,1),
	 (5,1),
	 (7,1),
	 (1,3),
	 (5,3),
	 (1,3),
	 (1,4),
	 (1,5),
	 (1,6),
	 (1,7);
INSERT INTO dbo.TeamUser (TeamMembersId,UserTeamsId) VALUES
	 (1,8),
	 (1,9),
	 (1,10);
INSERT INTO dbo.Panels (Id,Name,Description,IsDeleted,TrashId,Team,CreatedById,TeamId) VALUES
	 (1,N'Panel1',N'Descripcion Panel 1',0,NULL,N'Equipo 1',2,1),
	 (2,N'Panel2',N'Descripcion Panel 2',0,NULL,N'Equipo 1',2,1),
	 (3,N'Panel3',N'Descripcion Panel 3',0,NULL,N'Equipo 1',2,1),
	 (4,N'Panel4',N'Descripcion Panel 4',0,NULL,N'Equipo 2',2,2),
	 (5,N'Panel5',N'Descripcion Panel 5',0,NULL,N'Equipo 5',2,5),
	 (6,N'Panel6',N'Descripcion Panel 6',0,NULL,N'Equipo 4',2,4);
INSERT INTO dbo.Epic (Name,Priority,Description,ExpirationDate,FromPanelId) VALUES
	 (N'Epica1',0,N'DescripcionEpica 1','2025-12-20 00:00:00.0000000',1),
	 (N'Epica2',1,N'DescripcionEpica 2','2025-11-30 00:00:00.0000000',4),
	 (N'Epica3',2,N'DescripcionEpica 3','2025-11-30 00:00:00.0000000',5);
	INSERT INTO dbo.Tasks (Id,Name,Description,IsDeleted,TrashId,Precedence,PanelId,ExpirationDate,IsInEpic,ExpectedEffort,InvertedEffort,Ended,EpicId) VALUES
	 (1,N'Tarea 1',N'DescripcionTarea 1',0,NULL,2,5,'2025-11-29 00:00:00.0000000',1,2,0,0,3),
	 (2,N'Tarea 2',N'DescripcionTarea 2',0,NULL,0,5,'2025-11-28 00:00:00.0000000',0,4,0,0,NULL),
	 (3,N'Tarea 3',N'DescripcionTarea 2',0,NULL,0,5,'2025-01-23 00:00:00.0000000',1,10,0,0,3),
	 (4,N'Tarea 4',N'DescripcionTarea 4',0,NULL,1,5,'2025-04-30 00:00:00.0000000',0,1,0,0,NULL),
	 (5,N'Tarea5',N'DescripcionTarea 5',0,NULL,0,1,'2025-09-10 00:00:00.0000000',0,1,0,0,NULL),
	 (6,N'Tarea6',N'DescripcionTarea 6',0,NULL,0,1,'2025-09-19 00:00:00.0000000',0,2,0,0,NULL),
	 (7,N'Tarea7',N'DescripcionTarea 7',0,NULL,2,1,'2025-11-20 00:00:00.0000000',1,3,0,0,1),
	 (8,N'Tarea8',N'DescripcionTarea 8',0,NULL,0,1,'2025-12-24 00:00:00.0000000',1,2,0,0,1),
	 (9,N'Tarea9',N'DescripcionTarea 9',0,NULL,0,1,'2025-09-10 00:00:00.0000000',0,3,0,0,NULL),
	 (10,N'Tarea10',N'DescripcionTarea 10',0,NULL,1,1,'2025-09-19 00:00:00.0000000',1,3,0,0,1);
INSERT INTO dbo.Tasks (Id,Name,Description,IsDeleted,TrashId,Precedence,PanelId,ExpirationDate,IsInEpic,ExpectedEffort,InvertedEffort,Ended,EpicId) VALUES
	 (11,N'Tarea11',N'DescripcionTarea 11',0,NULL,2,2,'2025-11-20 00:00:00.0000000',0,4,0,0,NULL),
	 (12,N'Tarea12',N'DescripcionTarea 12',0,NULL,0,2,'2025-12-24 00:00:00.0000000',0,4,0,0,NULL),
	 (13,N'Tarea13',N'DescripcionTarea 13',0,NULL,0,2,'2025-09-10 00:00:00.0000000',0,5,0,0,NULL),
	 (14,N'Tarea14',N'DescripcionTarea 14',1,7,0,2,'2025-09-19 00:00:00.0000000',0,5,0,0,NULL),
	 (15,N'Tarea15',N'DescripcionTarea 15',0,NULL,0,3,'2025-11-20 00:00:00.0000000',0,5,0,0,NULL),
	 (16,N'Tarea16',N'DescripcionTarea 16',0,NULL,1,3,'2025-12-24 00:00:00.0000000',0,6,0,0,NULL),
	 (17,N'Tarea17',N'DescripcionTarea 17',0,NULL,0,4,'2025-09-10 00:00:00.0000000',0,6,0,0,NULL),
	 (18,N'Tarea18',N'DescripcionTarea 18',0,NULL,0,4,'2025-09-19 00:00:00.0000000',1,7,0,0,2),
	 (19,N'Tarea19',N'DescripcionTarea 19',0,NULL,2,4,'2025-11-20 00:00:00.0000000',0,7,0,0,NULL),
	 (20,N'Tarea20',N'DescripcionTarea 20',0,NULL,0,4,'2025-12-24 00:00:00.0000000',1,7,0,0,2);	
	
INSERT INTO dbo.Comments (CreatedById,Resolved,ResolvedById,TaskId,ResolvedTime,Content) VALUES
	 (7,1,5,5,'2024-11-20 08:18:55.0179853',N'Comentario1'),
	 (7,1,7,5,'2024-11-20 08:19:27.5446869',N'Comentario2'),
	 (7,0,NULL,5,'0001-01-01 00:00:00.0000000',N'Comentario3'),
	 (7,0,NULL,6,'0001-01-01 00:00:00.0000000',N'Comentario4'),
	 (7,0,NULL,6,'0001-01-01 00:00:00.0000000',N'Comentario5'),
	 (7,0,NULL,7,'0001-01-01 00:00:00.0000000',N'Comentario 6'),
	 (7,0,NULL,10,'0001-01-01 00:00:00.0000000',N'Comentario7'),
	 (7,0,NULL,10,'0001-01-01 00:00:00.0000000',N'Comentario8'),
	 (7,0,NULL,8,'0001-01-01 00:00:00.0000000',N'Comentario9'),
	 (7,0,NULL,8,'0001-01-01 00:00:00.0000000',N'Comentario10');
INSERT INTO dbo.Comments (CreatedById,Resolved,ResolvedById,TaskId,ResolvedTime,Content) VALUES
	 (5,0,NULL,17,'0001-01-01 00:00:00.0000000',N'Comentario 11'),
	 (5,0,NULL,17,'0001-01-01 00:00:00.0000000',N'Comentario 12'),
	 (5,0,NULL,12,'0001-01-01 00:00:00.0000000',N'Comentario13'),
	 (5,0,NULL,12,'0001-01-01 00:00:00.0000000',N'Comentario14'),
	 (5,0,NULL,13,'0001-01-01 00:00:00.0000000',N'Comentario15'),
	 (5,0,NULL,13,'0001-01-01 00:00:00.0000000',N'Comentario16');

INSERT INTO dbo.Notifications (Text,FromUserId,ToUserId) VALUES
	 (N'El Usuario Leo resolvio tu comentario',5,7),
	 (N'El Usuario Aitana resolvio tu comentario',7,7);

