USE [master]
GO
/****** Object:  Database [HRM_SWD392]    Script Date: 7/23/2023 9:05:26 PM ******/
CREATE DATABASE [HRM_SWD392]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'HRM_SWD392', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS_0001\MSSQL\DATA\HRM_SWD392.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'HRM_SWD392_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS_0001\MSSQL\DATA\HRM_SWD392_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [HRM_SWD392] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [HRM_SWD392].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [HRM_SWD392] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [HRM_SWD392] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [HRM_SWD392] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [HRM_SWD392] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [HRM_SWD392] SET ARITHABORT OFF 
GO
ALTER DATABASE [HRM_SWD392] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [HRM_SWD392] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [HRM_SWD392] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [HRM_SWD392] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [HRM_SWD392] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [HRM_SWD392] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [HRM_SWD392] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [HRM_SWD392] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [HRM_SWD392] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [HRM_SWD392] SET  DISABLE_BROKER 
GO
ALTER DATABASE [HRM_SWD392] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [HRM_SWD392] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [HRM_SWD392] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [HRM_SWD392] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [HRM_SWD392] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [HRM_SWD392] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [HRM_SWD392] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [HRM_SWD392] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [HRM_SWD392] SET  MULTI_USER 
GO
ALTER DATABASE [HRM_SWD392] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [HRM_SWD392] SET DB_CHAINING OFF 
GO
ALTER DATABASE [HRM_SWD392] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [HRM_SWD392] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [HRM_SWD392] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [HRM_SWD392] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [HRM_SWD392] SET QUERY_STORE = OFF
GO
USE [HRM_SWD392]
GO
/****** Object:  Table [dbo].[Attendance]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Attendance](
	[AttendanceId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[AttendanceDate] [date] NULL,
	[CheckInTime] [datetime] NULL,
	[CheckOutTime] [datetime] NULL,
	[DayIncome] [decimal](18, 0) NULL,
 CONSTRAINT [PK_Attendance] PRIMARY KEY CLUSTERED 
(
	[AttendanceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChangeWorkDepartmentRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChangeWorkDepartmentRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[ReasonForMoving] [nvarchar](300) NULL,
	[DepartmentMoveToId] [nvarchar](10) NULL,
	[DateWantToMove] [date] NULL,
	[DateCreateRequest] [datetime] NULL,
	[CurrentDepartmentId] [nvarchar](10) NULL,
	[HRID] [nvarchar](10) NULL,
	[ResponseRequest] [bit] NULL,
	[DateResponeRequest] [datetime] NULL,
	[DateMoveToNewDepartment] [date] NULL,
 CONSTRAINT [PK_ChangeWorkDepartmentRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Department]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Department](
	[DepartmentId] [nvarchar](10) NOT NULL,
	[DepartmentName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Department] PRIMARY KEY CLUSTERED 
(
	[DepartmentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee](
	[EmployeeId] [nvarchar](10) NOT NULL,
	[EmplyeeName] [nvarchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](30) NULL,
	[Role] [nvarchar](20) NULL,
	[DepartmentId] [nvarchar](10) NULL,
	[Phone] [nvarchar](10) NULL,
	[Address] [nvarchar](200) NULL,
	[Salary] [decimal](18, 0) NULL,
	[TaxCode] [nvarchar](20) NULL,
	[Level] [nvarchar](20) NULL,
	[ManagerId] [nvarchar](10) NULL,
	[DayOne] [date] NULL,
	[LastDay] [date] NULL,
 CONSTRAINT [PK_Employee] PRIMARY KEY CLUSTERED 
(
	[EmployeeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LeaveRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LeaveRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[DateOff] [date] NULL,
	[DaysLeave] [int] NULL,
	[Reason] [nvarchar](300) NULL,
	[HRID] [nvarchar](10) NULL,
	[Status] [nvarchar](20) NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_LeaveRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OTRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OTRequest](
	[EmployeeId] [nvarchar](10) NOT NULL,
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[DateRequest] [datetime] NULL,
	[TimeStartOT] [datetime] NULL,
	[TimeEndOT] [datetime] NULL,
	[TaskOT] [nvarchar](50) NULL,
	[RequestStatus] [nvarchar](20) NULL,
	[ApproveDate] [datetime] NULL,
	[ApproverId] [nvarchar](10) NULL,
	[Comment] [nvarchar](50) NULL,
	[OTIncome] [nvarchar](10) NULL,
 CONSTRAINT [PK_OTRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ResignationRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ResignationRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[RequestDate] [datetime] NULL,
	[LastWorkingDate] [date] NULL,
	[Reason] [nvarchar](300) NULL,
	[RequestStatus] [nvarchar](20) NULL,
	[ApproveDate] [datetime] NULL,
	[ApproverId] [nvarchar](10) NULL,
	[Comment] [nvarchar](100) NULL,
 CONSTRAINT [PK_ResignationRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TaxRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaxRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[RequestDate] [datetime] NULL,
	[TaxYear] [int] NULL,
	[TotalIncome] [decimal](18, 0) NULL,
	[Deductions] [decimal](18, 0) NULL,
	[TaxableIncome] [decimal](18, 0) NULL,
	[TaxAmount] [decimal](18, 0) NULL,
	[RequestStatus] [nvarchar](20) NULL,
	[ApproveDate] [datetime] NULL,
	[ApproverId] [nvarchar](10) NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_TaxRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpdateAttendanceRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdateAttendanceRequest](
	[RequestId] [int] NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[Date] [date] NOT NULL,
	[TimeIn] [datetime] NULL,
	[Reason] [nvarchar](300) NULL,
	[HRID] [nvarchar](10) NULL,
	[Status] [nvarchar](20) NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_UpdateAttendanceRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UpdateEmployeeInforRequest]    Script Date: 7/23/2023 9:05:26 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UpdateEmployeeInforRequest](
	[RequestId] [int] IDENTITY(1,1) NOT NULL,
	[EmployeeId] [nvarchar](10) NOT NULL,
	[EmplyeeName] [nvarchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[Gender] [int] NULL,
	[Email] [nvarchar](50) NULL,
	[Password] [nvarchar](30) NULL,
	[Role] [nvarchar](20) NULL,
	[DepartmentId] [nvarchar](10) NULL,
	[Phone] [nvarchar](10) NULL,
	[Address] [nvarchar](200) NULL,
	[Salary] [decimal](18, 0) NULL,
	[TaxCode] [nvarchar](20) NULL,
	[Level] [nvarchar](20) NULL,
	[ManagerId] [nvarchar](10) NULL,
	[DayOne] [date] NULL,
	[LastDay] [date] NULL,
	[ApproverId] [nvarchar](10) NULL,
	[Status] [nvarchar](20) NULL,
	[ApproveDate] [datetime] NULL,
	[Comment] [nvarchar](50) NULL,
 CONSTRAINT [PK_UpdateEmployeeInforRequest] PRIMARY KEY CLUSTERED 
(
	[RequestId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Attendance] ON 
GO
INSERT [dbo].[Attendance] ([AttendanceId], [EmployeeId], [AttendanceDate], [CheckInTime], [CheckOutTime], [DayIncome]) VALUES (1, N'EMP0016', CAST(N'2023-07-23' AS Date), CAST(N'2023-07-23T12:34:49.480' AS DateTime), CAST(N'2023-07-23T20:39:55.283' AS DateTime), CAST(545455 AS Decimal(18, 0)))
GO
INSERT [dbo].[Attendance] ([AttendanceId], [EmployeeId], [AttendanceDate], [CheckInTime], [CheckOutTime], [DayIncome]) VALUES (2, N'EMP0018', CAST(N'2023-07-23' AS Date), CAST(N'2023-07-23T20:34:49.520' AS DateTime), NULL, NULL)
GO
INSERT [dbo].[Attendance] ([AttendanceId], [EmployeeId], [AttendanceDate], [CheckInTime], [CheckOutTime], [DayIncome]) VALUES (3, N'EMP0020', CAST(N'2023-07-21' AS Date), CAST(N'2023-07-21T12:45:08.940' AS DateTime), CAST(N'2023-07-21T20:46:05.823' AS DateTime), CAST(545455 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[Attendance] OFF
GO
INSERT [dbo].[Employee] ([EmployeeId], [EmplyeeName], [DateOfBirth], [Gender], [Email], [Password], [Role], [DepartmentId], [Phone], [Address], [Salary], [TaxCode], [Level], [ManagerId], [DayOne], [LastDay]) VALUES (N'EMP0016', N'HR12345678', CAST(N'2001-06-14' AS Date), 1, N'hr@mail.com', N'1', N'HR', NULL, N'0111111111', N'HCM', CAST(12000000 AS Decimal(18, 0)), N'123121', N'2', N'EMP0018', CAST(N'2020-07-23' AS Date), NULL)
GO
INSERT [dbo].[Employee] ([EmployeeId], [EmplyeeName], [DateOfBirth], [Gender], [Email], [Password], [Role], [DepartmentId], [Phone], [Address], [Salary], [TaxCode], [Level], [ManagerId], [DayOne], [LastDay]) VALUES (N'EMP0018', N'HRM1231211', CAST(N'2001-06-14' AS Date), 1, N'hrm@gmail.com', N'1', N'HRManager', NULL, N'0111111111', N'HCM', CAST(12000000 AS Decimal(18, 0)), N'123121', N'2', NULL, CAST(N'2020-07-23' AS Date), NULL)
GO
INSERT [dbo].[Employee] ([EmployeeId], [EmplyeeName], [DateOfBirth], [Gender], [Email], [Password], [Role], [DepartmentId], [Phone], [Address], [Salary], [TaxCode], [Level], [ManagerId], [DayOne], [LastDay]) VALUES (N'EMP0020', N'EMP1234567', CAST(N'2001-02-23' AS Date), 2, N'emp@mail.com', N'1', N'Employee', NULL, N'0111111111', N'HCM', CAST(12000000 AS Decimal(18, 0)), N'123121', N'2', N'EMP0016', CAST(N'2020-07-23' AS Date), NULL)
GO
ALTER TABLE [dbo].[Attendance]  WITH CHECK ADD  CONSTRAINT [FK_Attendance_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Attendance] CHECK CONSTRAINT [FK_Attendance_Employee]
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_ChangeWorkDepartmentRequest_Department] FOREIGN KEY([DepartmentMoveToId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest] CHECK CONSTRAINT [FK_ChangeWorkDepartmentRequest_Department]
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_ChangeWorkDepartmentRequest_Department1] FOREIGN KEY([CurrentDepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest] CHECK CONSTRAINT [FK_ChangeWorkDepartmentRequest_Department1]
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_ChangeWorkDepartmentRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest] CHECK CONSTRAINT [FK_ChangeWorkDepartmentRequest_Employee]
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest]  WITH CHECK ADD  CONSTRAINT [FK_ChangeWorkDepartmentRequest_Employee1] FOREIGN KEY([HRID])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ChangeWorkDepartmentRequest] CHECK CONSTRAINT [FK_ChangeWorkDepartmentRequest_Employee1]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Department] FOREIGN KEY([DepartmentId])
REFERENCES [dbo].[Department] ([DepartmentId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Department]
GO
ALTER TABLE [dbo].[Employee]  WITH CHECK ADD  CONSTRAINT [FK_Employee_Employee] FOREIGN KEY([ManagerId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[Employee] CHECK CONSTRAINT [FK_Employee_Employee]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Employee]
GO
ALTER TABLE [dbo].[LeaveRequest]  WITH CHECK ADD  CONSTRAINT [FK_LeaveRequest_Employee1] FOREIGN KEY([HRID])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[LeaveRequest] CHECK CONSTRAINT [FK_LeaveRequest_Employee1]
GO
ALTER TABLE [dbo].[OTRequest]  WITH CHECK ADD  CONSTRAINT [FK_OTRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[OTRequest] CHECK CONSTRAINT [FK_OTRequest_Employee]
GO
ALTER TABLE [dbo].[OTRequest]  WITH CHECK ADD  CONSTRAINT [FK_OTRequest_Employee1] FOREIGN KEY([ApproverId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[OTRequest] CHECK CONSTRAINT [FK_OTRequest_Employee1]
GO
ALTER TABLE [dbo].[ResignationRequest]  WITH CHECK ADD  CONSTRAINT [FK_ResignationRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ResignationRequest] CHECK CONSTRAINT [FK_ResignationRequest_Employee]
GO
ALTER TABLE [dbo].[ResignationRequest]  WITH CHECK ADD  CONSTRAINT [FK_ResignationRequest_Employee1] FOREIGN KEY([ApproverId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[ResignationRequest] CHECK CONSTRAINT [FK_ResignationRequest_Employee1]
GO
ALTER TABLE [dbo].[TaxRequest]  WITH CHECK ADD  CONSTRAINT [FK_TaxRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[TaxRequest] CHECK CONSTRAINT [FK_TaxRequest_Employee]
GO
ALTER TABLE [dbo].[TaxRequest]  WITH CHECK ADD  CONSTRAINT [FK_TaxRequest_Employee1] FOREIGN KEY([ApproverId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[TaxRequest] CHECK CONSTRAINT [FK_TaxRequest_Employee1]
GO
ALTER TABLE [dbo].[UpdateAttendanceRequest]  WITH CHECK ADD  CONSTRAINT [FK__UpdateAtt__EmployeeID] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[UpdateAttendanceRequest] CHECK CONSTRAINT [FK__UpdateAtt__EmployeeID]
GO
ALTER TABLE [dbo].[UpdateAttendanceRequest]  WITH CHECK ADD  CONSTRAINT [FK__UpdateAtt__HRID] FOREIGN KEY([HRID])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[UpdateAttendanceRequest] CHECK CONSTRAINT [FK__UpdateAtt__HRID]
GO
ALTER TABLE [dbo].[UpdateEmployeeInforRequest]  WITH CHECK ADD  CONSTRAINT [FK_UpdateEmployeeInforRequest_Employee] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[UpdateEmployeeInforRequest] CHECK CONSTRAINT [FK_UpdateEmployeeInforRequest_Employee]
GO
ALTER TABLE [dbo].[UpdateEmployeeInforRequest]  WITH CHECK ADD  CONSTRAINT [FK_UpdateEmployeeInforRequest_Employee1] FOREIGN KEY([ApproverId])
REFERENCES [dbo].[Employee] ([EmployeeId])
GO
ALTER TABLE [dbo].[UpdateEmployeeInforRequest] CHECK CONSTRAINT [FK_UpdateEmployeeInforRequest_Employee1]
GO
USE [master]
GO
ALTER DATABASE [HRM_SWD392] SET  READ_WRITE 
GO
