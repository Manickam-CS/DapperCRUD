CREATE DATABASE [EmployeeDB_Dapper]
GO

USE [EmployeeDB_Dapper]
GO
/****** Object:  Table [dbo].[Departments]    Script Date: 10/27/2021 12:55:02 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departments](
	[DeptId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
 CONSTRAINT [PK_Departments] PRIMARY KEY CLUSTERED 
(
	[DeptId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 10/27/2021 12:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](500) NOT NULL,
	[Address] [nvarchar](max) NOT NULL,
	[ImagePath] [nvarchar](max) NOT NULL,
	[DeptId] [int] NOT NULL,
 CONSTRAINT [PK_Employees] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employees]  WITH CHECK ADD  CONSTRAINT [FK_Employees_Departments_DeptId] FOREIGN KEY([DeptId])
REFERENCES [dbo].[Departments] ([DeptId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Employees] CHECK CONSTRAINT [FK_Employees_Departments_DeptId]
GO
/****** Object:  StoredProcedure [dbo].[usp_AddDepartment]    Script Date: 10/27/2021 12:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_AddDepartment]     
(    
    @Name varchar(100)
)    
as     
Begin     
    Insert into Departments([Name]) Values (@Name)     
End
GO
/****** Object:  StoredProcedure [dbo].[usp_AddEmployee]    Script Date: 10/27/2021 12:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_AddEmployee]     
(    
    @Name VARCHAR(500),     
    @Address nvarchar(max),    
    @ImagePath nvarchar(max),    
    @DeptId int,
	@EmpId int OUTPUT
)    
as     
Begin     
    Insert into Employees([Name],[Address],[ImagePath], [DeptId]) Values (@Name,@Address,@ImagePath, @DeptId)  
	SELECT @EmpId = @@identity;
End
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateDepartment]    Script Date: 10/27/2021 12:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_UpdateDepartment]      
(     
	@DeptId int,
    @Name VARCHAR(100) 
   
)      
as      
begin      
   Update Departments set [Name]=@Name where [DeptId]=@DeptId     
End 
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateEmployee]    Script Date: 10/27/2021 12:55:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE procedure [dbo].[usp_UpdateEmployee]      
(     
	@EmpId int,
    @Name VARCHAR(500),     
    @Address nvarchar(max),    
    @ImagePath nvarchar(max),    
    @DeptId int     
)      
as      
begin      
   Update Employees set [Name]=@Name, [Address]=@Address, ImagePath=@ImagePath, DeptId=@DeptId where EmpId=@EmpId      
End 
GO
