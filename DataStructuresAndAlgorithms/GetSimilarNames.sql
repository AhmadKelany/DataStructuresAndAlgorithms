-- the prompt:
/* 
in SQL server, I have a table: People, with two columns:Id (int), Name (nvarchar(60))
sample data: 
Id : Name
1  : ahmed hassan mohamed
2  : hassan mohamed omar
3  : yousef tamer kamel
4  : yousef tamer ibrahim
5  : yosef ibrahim tamer
6  : ahmed hassan mohamed

write a SQL function named: GetPeopleWithSimilarNames() that takes a parameter: @namesCount (int) and returns a table with the following columns: GroupId, Id, Name
so that for each group, names with common consecutive @namesCount have the same group id.

e.g.: 
Select * from dbo.GetPeopleWithSimilarNames(2) 

should return :
group id : Id : name
1        : 1  : ahmed hassan mohamed
1        : 2  : hassan mohamed omar
1        : 6  : ahmed hassan mohamed
2        : 3  : yousef tamer kamel
2        : 4  : yousef tamer ibrahim

*/

-- the answer:
/* 
To solve this task, we need to create a SQL function that groups people by common consecutive names. This requires a good understanding of SQL string manipulation and comparison functions. 

Here's a basic outline of how we can approach this:

1. Split the names into individual words.
2. Create a table that contains each unique pair of consecutive words and their corresponding IDs.
3. Group the IDs by the unique pairs of words.

However, SQL Server doesn't have built-in functions for splitting strings into words or for comparing sequences of words. We'll need to create custom functions or stored procedures for these tasks.

Let's start by creating a function to split the names into words. This function will take a string and return a table with each word in a separate row. We'll use a common table expression (CTE) to recursively split the string by spaces.

Here's the code for the function:
*/

CREATE FUNCTION dbo.SplitString
(
    @str NVARCHAR(MAX),
    @delimiter CHAR(1)
)
RETURNS @returnTable TABLE (Word NVARCHAR(MAX))
AS
BEGIN
    DECLARE @pos INT, @slice NVARCHAR(MAX)

    -- Loop through the string to extract each word
    WHILE CHARINDEX(@delimiter, @str) > 0
    BEGIN
        SELECT @pos  = CHARINDEX(@delimiter, @str)  
        SELECT @slice = LEFT(@str, @pos - 1)

        INSERT INTO @returnTable
        VALUES (@slice)

        SELECT @str = RIGHT(@str, LEN(@str) - @pos)
        IF LEN(@str) = 0 BREAK
    END

    INSERT INTO @returnTable
    VALUES (@str)

    RETURN
END

/*
Next, we'll create a function to compare sequences of words. This function will take two strings and a number, and return a boolean indicating whether the strings have the same sequence of words of the given length. We'll use the `SplitString` function to split the strings into words, and then compare the sequences of words.

Here's the code for the function:
*/

CREATE FUNCTION dbo.CompareSequences
(
    @str1 NVARCHAR(MAX),
    @str2 NVARCHAR(MAX),
    @length INT
)
RETURNS BIT
AS
BEGIN
    DECLARE @words1 TABLE (Word NVARCHAR(MAX), Position INT)
    DECLARE @words2 TABLE (Word NVARCHAR(MAX), Position INT)

    INSERT INTO @words1
    SELECT Word, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Position
    FROM dbo.SplitString(@str1, ' ')

    INSERT INTO @words2
    SELECT Word, ROW_NUMBER() OVER (ORDER BY (SELECT NULL)) AS Position
    FROM dbo.SplitString(@str2, ' ')

    DECLARE @result BIT = 1

    DECLARE @i INT = 1
    WHILE @i <= @length
    BEGIN
        IF NOT EXISTS (
            SELECT 1
            FROM @words1 w1
            JOIN @words2 w2 ON w1.Position = w2.Position
            WHERE w1.Word = w2.Word AND w1.Position = @i
        )
        BEGIN
            SET @result = 0
            BREAK
        END

        SET @i = @i + 1
    END

    RETURN @result
END

/*
Finally, we'll create the `GetPeopleWithSimilarNames` function. This function will take a number and return a table with the IDs and names of people who have the same sequence of words of the given length in their names. We'll use the `CompareSequences` function to compare the sequences of words.

Here's the code for the function:
*/

CREATE FUNCTION dbo.GetPeopleWithSimilarNames
(
    @namesCount INT
)
RETURNS @returnTable TABLE (GroupId INT, Id INT, Name NVARCHAR(MAX))
AS
BEGIN
    DECLARE @people TABLE (Id INT, Name NVARCHAR(MAX), Words TABLE (Word NVARCHAR(MAX), Position INT))
    DECLARE @groups TABLE (GroupId INT, Id INT, Name NVARCHAR(MAX))

    INSERT INTO @people
    SELECT Id, Name
    FROM People

    DECLARE @id INT, @name NVARCHAR(MAX)
    DECLARE @groupId INT = 1

    WHILE EXISTS (SELECT 1 FROM @people)
    BEGIN
        SELECT TOP 1 @id = Id, @name = Name
        FROM @people

        INSERT INTO @groups
        SELECT @groupId, Id, Name
        FROM @people
        WHERE dbo.CompareSequences(Name, @name, @namesCount) = 1

        DELETE FROM @people
        WHERE dbo.CompareSequences(Name, @name, @namesCount) = 1

        SET @groupId = @groupId + 1
    END

    INSERT INTO @returnTable
    SELECT GroupId, Id, Name
    FROM @groups

    RETURN
END

/*
You can use the `GetPeopleWithSimilarNames` function like this:
*/

SELECT *
FROM dbo.GetPeopleWithSimilarNames(2)

/*
This will return a table with the IDs and names of people who have the same sequence of two words in their names, grouped by the sequence of words.

Please note that this solution assumes that the names are case-insensitive. If the names are case-sensitive, you'll need to modify the `CompareSequences` function to compare the words in a case-sensitive manner.

Also, this solution doesn't handle names with punctuation or special characters. If the names can contain punctuation or special characters, you'll need to modify the `SplitString` function to split the strings by spaces and punctuation or special characters.

Finally, this solution doesn't handle names with different numbers of words. If the names can have different numbers of words, you'll need to modify the `CompareSequences` function to compare the sequences of words in a way that handles different numbers of words.

I hope this helps! Let me know if you have any questions.
*/
