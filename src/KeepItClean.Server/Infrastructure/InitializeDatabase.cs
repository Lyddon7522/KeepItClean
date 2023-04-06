using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.Model;

namespace KeepItClean.Server.Infrastructure;

public class InitializeDatabaseService
{
    private readonly IAmazonDynamoDB _amazonDynamoDb;
    private const string _tableName = "Locations";

    public InitializeDatabaseService(IAmazonDynamoDB amazonDynamoDb)
    {
        _amazonDynamoDb = amazonDynamoDb;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken)
    {
        if (await LocationTableAlreadyExistsAsync(cancellationToken)) return;

        // TODO: modify this to create a real Locations table.
        var request = new CreateTableRequest
        {
            TableName = _tableName,
            AttributeDefinitions = new List<AttributeDefinition>()
            {
                new AttributeDefinition
                {
                  AttributeName = "Id",
                  AttributeType = "N"
                }
            },
            KeySchema = new List<KeySchemaElement>
            {
                new KeySchemaElement
                {
                    AttributeName = "Id",
                    KeyType = "HASH"  //Partition key - probably should be US state?
                }
            },
            ProvisionedThroughput = new ProvisionedThroughput
            {
                ReadCapacityUnits = 10,
                WriteCapacityUnits = 5
            }
        };

        await _amazonDynamoDb.CreateTableAsync(request, cancellationToken);
    }

    private async Task<bool> LocationTableAlreadyExistsAsync(CancellationToken cancellationToken)
    {
        DescribeTableResponse? existingTable = null;
        try
        {
            existingTable = await _amazonDynamoDb.DescribeTableAsync(_tableName, cancellationToken);
        }
        catch (ResourceNotFoundException) { }
        if (existingTable is not null) return true;

        return false;
    }
}