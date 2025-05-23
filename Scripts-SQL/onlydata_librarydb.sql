USE [Library]
GO
SET IDENTITY_INSERT [dbo].[Books] ON 

INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (1, N'El Principito', N'Antoine de Saint-Exupery', 2001, N'9788478887194', N'Novela', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (2, N'Habitos Atomicos', N'James Clear', 2020, N'9788418118036', N'Autoayuda', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (3, N'Sorprende a tu mente', N'Ana Ibáñez', 2025, N'9788408301776', N'Autoayuda', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (4, N'EL ARTE DE LA GUERRA', N'Sun Tzu', 2010, N'9788494709203', N'Filosofia', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (5, N'LA ERA DE LA INTELIGENCIA ARTIFICIAL Y NUESTRO FUTURO HUMANO', N'Henry A. Kissinger, ERIC SCHIMIDT y DANIEL HUTTENLOCHER', 2023, N'9788441548503', N'Informatica', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (6, N'PIENSA CLARO', N'KIKO LLANERAS', 2022, N'9788418967078', N'Informatica', 1)
INSERT [dbo].[Books] ([Id], [Title], [Author], [YearPublication], [ISBN], [Genre], [Available]) VALUES (7, N'LA BELLA Y LA BESTIA', N'Madame Jeanne Marie Leprince de Beaumont', 2013, N'9788494094057', N'Fantasia', 1)
SET IDENTITY_INSERT [dbo].[Books] OFF
GO
