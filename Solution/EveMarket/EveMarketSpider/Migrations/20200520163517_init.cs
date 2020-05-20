using Microsoft.EntityFrameworkCore.Migrations;

namespace EveMarketSpider.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Attributes",
                columns: table => new
                {
                    AttributeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    DefaultValue = table.Column<float>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    HighIsGood = table.Column<bool>(nullable: false),
                    IconId = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Stackable = table.Column<bool>(nullable: false),
                    UnitId = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attributes", x => x.AttributeId);
                });

            migrationBuilder.CreateTable(
                name: "Constellations",
                columns: table => new
                {
                    ConstellationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Constellations", x => x.ConstellationId);
                });

            migrationBuilder.CreateTable(
                name: "Effects",
                columns: table => new
                {
                    EffectId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DisallowAutoRepeat = table.Column<bool>(nullable: false),
                    DischargeAttributeId = table.Column<int>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    DurationAttributeId = table.Column<int>(nullable: false),
                    EffectCategory = table.Column<int>(nullable: false),
                    ElectronicChance = table.Column<bool>(nullable: false),
                    FalloffAttributeId = table.Column<int>(nullable: false),
                    IconId = table.Column<int>(nullable: false),
                    IsAssistance = table.Column<bool>(nullable: false),
                    IsOffensive = table.Column<bool>(nullable: false),
                    IsWarpSafe = table.Column<bool>(nullable: false),
                    PostExpression = table.Column<int>(nullable: false),
                    PreExpression = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    RangeAttributeId = table.Column<int>(nullable: false),
                    RangeChance = table.Column<bool>(nullable: false),
                    TrackingSpeedAttributeId = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Effects", x => x.EffectId);
                });

            migrationBuilder.CreateTable(
                name: "MarketGroups",
                columns: table => new
                {
                    MarketGroupId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    ParentGroupId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketGroups", x => x.MarketGroupId);
                });

            migrationBuilder.CreateTable(
                name: "Modifiers",
                columns: table => new
                {
                    Func = table.Column<string>(nullable: false),
                    Domain = table.Column<string>(nullable: true),
                    effect_id = table.Column<int>(nullable: false),
                    ModifiedAttributeId = table.Column<int>(nullable: false),
                    ModifyingAttributeId = table.Column<int>(nullable: false),
                    Operator = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modifiers", x => x.Func);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    RegionId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.RegionId);
                });

            migrationBuilder.CreateTable(
                name: "Stations",
                columns: table => new
                {
                    StationId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    MaxDockableShipVolume = table.Column<float>(nullable: false),
                    SystemId = table.Column<int>(nullable: false),
                    TypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stations", x => x.StationId);
                });

            migrationBuilder.CreateTable(
                name: "Systems",
                columns: table => new
                {
                    SystemId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    ConstellationId = table.Column<int>(nullable: false),
                    SecurityClass = table.Column<string>(nullable: true),
                    SecurityStatus = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Systems", x => x.SystemId);
                });

            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Etag = table.Column<string>(nullable: true),
                    Capacity = table.Column<float>(nullable: false),
                    GroupId = table.Column<int>(nullable: false),
                    GraphicId = table.Column<int>(nullable: false),
                    IconId = table.Column<int>(nullable: false),
                    MarketGroupId = table.Column<int>(nullable: false),
                    Mass = table.Column<float>(nullable: false),
                    PackagedVolume = table.Column<float>(nullable: false),
                    PortionSize = table.Column<int>(nullable: false),
                    Published = table.Column<bool>(nullable: false),
                    Radius = table.Column<float>(nullable: false),
                    Volume = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.TypeId);
                });

            migrationBuilder.CreateTable(
                name: "TypeAttribute",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false),
                    AttributeId = table.Column<int>(nullable: false),
                    Value = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeAttribute", x => new { x.TypeId, x.AttributeId });
                    table.ForeignKey(
                        name: "FK_TypeAttribute_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TypeEffect",
                columns: table => new
                {
                    TypeId = table.Column<int>(nullable: false),
                    EffectId = table.Column<int>(nullable: false),
                    IsDefault = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TypeEffect", x => new { x.TypeId, x.EffectId });
                    table.ForeignKey(
                        name: "FK_TypeEffect_Types_TypeId",
                        column: x => x.TypeId,
                        principalTable: "Types",
                        principalColumn: "TypeId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attributes");

            migrationBuilder.DropTable(
                name: "Constellations");

            migrationBuilder.DropTable(
                name: "Effects");

            migrationBuilder.DropTable(
                name: "MarketGroups");

            migrationBuilder.DropTable(
                name: "Modifiers");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Stations");

            migrationBuilder.DropTable(
                name: "Systems");

            migrationBuilder.DropTable(
                name: "TypeAttribute");

            migrationBuilder.DropTable(
                name: "TypeEffect");

            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
