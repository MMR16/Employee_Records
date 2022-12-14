USE [master]
GO
/****** Object:  Database [Employee_Records]    Script Date: 11/22/2022 12:05:10 PM ******/
CREATE DATABASE [Employee_Records]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Employee_Records', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Employee_Records.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Employee_Records_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Employee_Records_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Employee_Records] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Employee_Records].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Employee_Records] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Employee_Records] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Employee_Records] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Employee_Records] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Employee_Records] SET ARITHABORT OFF 
GO
ALTER DATABASE [Employee_Records] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Employee_Records] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Employee_Records] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Employee_Records] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Employee_Records] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Employee_Records] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Employee_Records] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Employee_Records] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Employee_Records] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Employee_Records] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Employee_Records] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Employee_Records] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Employee_Records] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Employee_Records] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Employee_Records] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Employee_Records] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Employee_Records] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Employee_Records] SET RECOVERY FULL 
GO
ALTER DATABASE [Employee_Records] SET  MULTI_USER 
GO
ALTER DATABASE [Employee_Records] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Employee_Records] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Employee_Records] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Employee_Records] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Employee_Records] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Employee_Records] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'Employee_Records', N'ON'
GO
ALTER DATABASE [Employee_Records] SET QUERY_STORE = OFF
GO
USE [Employee_Records]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](500) NOT NULL,
	[DateOfBirth] [date] NULL,
	[Adress] [nvarchar](1000) NULL,
	[DepartmentId] [int] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EmployeeFiles]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EmployeeFiles](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FileName] [nvarchar](500) NULL,
	[Filepath] [nvarchar](max) NULL,
	[EmployeeId] [int] NULL,
 CONSTRAINT [PK_EmployeeFiles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Department] ON 

INSERT [dbo].[Department] ([Id], [Name]) VALUES (1, N'Android')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (2, N'Desktop')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (3, N'IOS')
INSERT [dbo].[Department] ([Id], [Name]) VALUES (4, N'Windows')
SET IDENTITY_INSERT [dbo].[Department] OFF
GO
SET IDENTITY_INSERT [dbo].[Employee] ON 

INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (27, N'string', CAST(N'2022-11-18' AS Date), N'string', 2)
INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (28, N'string', CAST(N'2022-11-18' AS Date), N'string', 1)
INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (42, N'Ahmed', CAST(N'2022-10-31' AS Date), N'Cairo', 3)
INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (43, N'Mostafa', CAST(N'2017-11-08' AS Date), N'Alex', 4)
INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (44, N'Alaa', CAST(N'2020-11-03' AS Date), N'Suiz', 1)
INSERT [dbo].[Employee] ([Id], [Name], [DateOfBirth], [Adress], [DepartmentId]) VALUES (45, N'Ali', CAST(N'2009-11-08' AS Date), N'banha', 2)
SET IDENTITY_INSERT [dbo].[Employee] OFF
GO
ALTER TABLE [dbo].[EmployeeFiles]  WITH CHECK ADD  CONSTRAINT [FK_EmployeeFiles_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[EmployeeFiles] CHECK CONSTRAINT [FK_EmployeeFiles_Employee]
GO
/****** Object:  StoredProcedure [dbo].[AddDepartment]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL

CREATE PROCEDURE [dbo].[AddDepartment] @deptName nvarchar(500) as
INSERT INTO Department(Name)
VALUES (@deptName)
GO
/****** Object:  StoredProcedure [dbo].[AddEmp]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[AddEmp]
@empName nvarchar(500), @empDateOfBirth Date, @empAdress nvarchar(1000), @DeptId int as
INSERT INTO Employee(Name,DateOfBirth,Adress,DepartmentId)
VALUES (@empName,@empDateOfBirth,@empAdress,@DeptId)
GO
/****** Object:  StoredProcedure [dbo].[AddEmpFile]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddEmpFile]
@fileName nvarchar(500), @filepath nvarchar(MAX), @EmployeeId int as
INSERT INTO EmployeeFiles(FileName,FilePath,EmployeeId)
VALUES (@fileName,@filepath,@EmployeeId)
GO
/****** Object:  StoredProcedure [dbo].[DeleteEmp]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteEmp] 
@empId int as delete from Employee where Id=@empId
GO
/****** Object:  StoredProcedure [dbo].[EditEmp]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[EditEmp] 
@empId int ,@empName nvarchar(500), @empDateOfBirth Date, @empAdress nvarchar(1000), @DeptId int as
Update  Employee set 
Name = @empName
,DateOfBirth =@empDateOfBirth
,Adress =@empAdress
,DepartmentId =@DeptId
where Employee.Id=@empId
GO
/****** Object:  StoredProcedure [dbo].[GetDept]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create Proc  [dbo].[GetDept] @deptId int as
select Name from Department where Id=@deptId
GO
/****** Object:  StoredProcedure [dbo].[GetDeptByName]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc  [dbo].[GetDeptByName] @deptName nvarchar(max) as
select Id from Department where Name=@deptName
GO
/****** Object:  StoredProcedure [dbo].[GetDepts]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Proc  [dbo].[GetDepts] as
select * from Department 
GO
/****** Object:  StoredProcedure [dbo].[GetEmp]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc  [dbo].[GetEmp] @empId int as
select emp.Id, emp.Name,emp.Adress,emp.DateOfBirth,dept.Name as Department
from Employee as emp inner join Department as dept on emp.DepartmentId=dept.Id 
where emp.Id=@empId
GO
/****** Object:  StoredProcedure [dbo].[GetEmpFiles]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Proc  [dbo].[GetEmpFiles] @empId int as
select *
from EmployeeFiles where EmployeeId=@empId
GO
/****** Object:  StoredProcedure [dbo].[GetEmps]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[GetEmps] as
(select emp.Id, emp.Name,emp.Adress,emp.DateOfBirth,dept.Name as Department
from Employee as emp 
inner join Department as dept on emp.DepartmentId = dept.Id ) 
GO
/****** Object:  StoredProcedure [dbo].[SearchEmp]    Script Date: 11/22/2022 12:05:10 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE Proc [dbo].[SearchEmp] @empName nvarchar(500) as
select emp.id, emp.Name,emp.Adress,emp.DateOfBirth,dept.Name as Department
from Employee as emp 
inner join Department as dept on emp.DepartmentId = dept.Id 
where emp.Name like '%' +  @empName + '%'
GO
USE [master]
GO
ALTER DATABASE [Employee_Records] SET  READ_WRITE 
GO
