----------GRADO 
-------------------------Kinder
Insert into Grado(Nombre, Nivel, Estado) Values ('Kinder', 'Educacion inicial', 1)
												GO
-------------------------------PRIMER CICLO
Insert into Grado(Nombre, Nivel, Estado) Values ('Primer Grado', 'Primer Ciclo', 1),
												('Segundo Grado', 'Primer Ciclo', 1),
												('Tercer Grado', 'Primer Ciclo', 1);
												GO
------------------SEGUNDO CICLO
Insert into Grado(Nombre, Nivel, Estado) Values ('Cuarto Grado', 'Segundo Ciclo', 1),
												('Quinto Grado', 'Segundo Ciclo', 1),
												('Sexto Grado', 'Segundo Ciclo', 1);
												GO
------------------Tercer CICLO
Insert into Grado(Nombre, Nivel, Estado) Values ('Septimo Grado', 'Tercer Ciclo', 1),
												('Octavo Grado', 'Tercer Ciclo', 1),
												('Noveno Grado', 'Tercer Ciclo', 1);

												GO
------------------EDUCACION MEDIA
Insert into Grado(Nombre, Nivel, Estado) Values ('Bachillerato General', 'Educacion Media', 1);


-------------------------------------------------------------TURNO

INSERT INTO Turno (Nombre, Estado) Values('Matutino', 1),
									     ('Vespertino', 1);

										 SELECT * FROM Turno


