CREATE TABLE Customer 
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Nama VARCHAR(32) NOT NULL,
	Email VARCHAR(32) NOT NULL,
	Alamat VARCHAR(255) NOT NULL,
	NoHandphone VARCHAR(32) NOT NULL,
	[Password] VARCHAR(10) NOT NULL,
	FotoProfile VARCHAR(255)
)

INSERT INTO Customer VALUES
('Putu', 'putu.duta@gmail.com', 'Jakarta', '088888', 'Putu123#', 'putud.jpg')

CREATE TABLE Pharmacy 
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	NamaPharmacy VARCHAR(32),
	EmailPharmacy VARCHAR(32),
	Alamat VARCHAR(255),
	NoTelephone VARCHAR(32),
	NamaPIC VARCHAR(32),
	EmailPIC VARCHAR(32),
	[Password] VARCHAR(10),
	FotoPharmacy VARCHAR(255)
)


INSERT INTO Pharmacies VALUES
('Apotek 1', 'apotek1@gmail.com', 'jl anak karyawan', '8131048912', 'Andi', 'andi@gmail.com', 'andi12', )

CREATE TABLE Products
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	Nama VARCHAR(32) NOT NULL,
	Deskripsi VARCHAR(255) NOT NULL,
	Stock INT NOT NULL,
	Price BIGINT NOT NULL,
	Category VARCHAR(10),
	[Type] VARCHAR(10) NOT NULL,
	PharmacyId INT FOREIGN KEY REFERENCES 	Pharmacy(Id) ON DELETE CASCADE
)

CREATE TABLE ShoppingCart
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	CustomerId INT FOREIGN KEY REFERENCES Customer(Id) ON DELETE CASCADE, 
	ProductId INT FOREIGN KEY REFERENCES 	Products(Id) ON DELETE CASCADE,
	Quantity INT 
)


CREATE TABLE HeaderTransaction
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	CustomerId INT FOREIGN KEY REFERENCES Customer(Id) ON DELETE CASCADE, 
	TransactionDate VARCHAR(255),
	PaymentType VARCHAR(255),
	BankName VARCHAR(255),
	BankAccountName VARCHAR(255),
	BankAccountNumber VARCHAR(255),
	TransferNominal VARCHAR(255),
	TransferProof VARCHAR(255),
	[Status] VARCHAR(255)
)

CREATE TABLE DetailTransaction
(
	Id INT IDENTITY(1, 1) PRIMARY KEY,
	TransactionId INT FOREIGN KEY REFERENCES HeaderTransaction(Id) ON DELETE CASCADE, 
	ProductId INT FOREIGN KEY REFERENCES 	Products(Id) ON DELETE CASCADE,
	Quantity INT
)


GO
CREATE PROCEDURE AddTransaction 
	@CustomerId INT, 
	@TransactionDate VARCHAR(255),
	@PaymentType VARCHAR(255),
	@BankName VARCHAR(255),
	@BankAccountName VARCHAR(255),
	@BankAccountNumber VARCHAR(255),
	@TransferNominal VARCHAR(255),
	@TransferProof VARCHAR(255),
	@ProductId INT,
	@Quantity INT
AS
BEGIN
	SET NOCOUNT ON;

	DECLARE @TransactionId INT

	INSERT INTO HeaderTransaction(CustomerId, TransactionDate, PaymentType, BankName, BankAccountName, BankAccountNumber, TransferNominal, TransferProof, Status) VALUES (
			@CustomerId, 
			@TransactionDate,
			@PaymentType,
			@BankName,
			@BankAccountName,
			@BankAccountNumber,
			@TransferNominal,
			@TransferProof,
			'PAID'
	)

	SET @TransactionId = (SELECT Id FROM HeaderTransaction WHERE Id=(SELECT max(Id) FROM HeaderTransaction))

	INSERT INTO DetailTransaction (ProductId, TransactionId, Quantity) VALUES (
			@ProductId,
			@TransactionId,
			@Quantity
	)
END

GO
CREATE PROCEDURE UpdateTransactionStatus
	@TransactionId INT,
	@Status VARCHAR(255)
AS
BEGIN
	SET NOCOUNT ON;

	UPDATE HeaderTransaction
	SET Status = @Status
	WHERE Id = @TransactionId
END