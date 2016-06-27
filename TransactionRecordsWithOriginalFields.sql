DROP TABLE TransactionRecords
GO

CREATE TABLE TransactionRecords (
	txTime			DATETIME,
	firstName		VARCHAR(128),
	lastName		VARCHAR(128),
	sessionID		VARCHAR(128),
	transactionID		VARCHAR(128) PRIMARY KEY,
	totalTickets		INT,
	amount			MONEY,
	custom			VARCHAR(128),
	buyerEmail		VARCHAR(128),
	paymentStatus		VARCHAR(128)
);

INSERT INTO TransactionRecords
VALUES('20160218 9:23:17 PM', 'Jones', 'Rumpel', 'uxahljndo5t00men5sm50nqz', 'x02301923091231', 5, 250, 'customfield', 'rumpel@hotmail.com', 'approved')
INSERT INTO TransactionRecords
VALUES('20160107 12:02:39 AM', 'Brown', 'Murphy', '4ftms2nk4i0yrf1y1wpiljfh', 'x43534536457464', 3, 150, 'customfield', 'murphybrown@hotmail.com', 'approved')
INSERT INTO TransactionRecords
VALUES('20160124 3:33:12 AM', 'Francis', 'Montgomery', ' bc0msyvowk3qtjr145dtdno2', 'x87654341211221', 1, 50, 'customfield', 'francis@hotmail.com', 'approved')
INSERT INTO TransactionRecords
VALUES('20160113 2:45:09 PM', 'Bennett', 'Jason', 'gz3gy3gv4extkeye4pesodnx', 'x1231231231232', 2, 100, 'customfield', 'jason@hotmail.com', 'approved')

SELECT * FROM TransactionRecords