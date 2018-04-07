using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TraumaNetwork.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AgeGroup",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgeGroup", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Agencies",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Website = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FinancialPlan",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FinancialPlan", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Service",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Service", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Specialty",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Order = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialty", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "AgencyAgeGroup",
                columns: table => new
                {
                    AgencyID = table.Column<Guid>(nullable: false),
                    AgeGroupID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyAgeGroup", x => new { x.AgencyID, x.AgeGroupID });
                    table.UniqueConstraint("AK_AgencyAgeGroup_AgeGroupID_AgencyID", x => new { x.AgeGroupID, x.AgencyID });
                    table.ForeignKey(
                        name: "FK_AgencyAgeGroup_AgeGroup_AgeGroupID",
                        column: x => x.AgeGroupID,
                        principalTable: "AgeGroup",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyAgeGroup_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyLocation",
                columns: table => new
                {
                    ID = table.Column<Guid>(nullable: false),
                    Address1 = table.Column<string>(nullable: true),
                    Address2 = table.Column<string>(nullable: true),
                    AgencyID = table.Column<Guid>(nullable: false),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Zip = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyLocation", x => x.ID);
                    table.ForeignKey(
                        name: "FK_AgencyLocation_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyCategory",
                columns: table => new
                {
                    AgencyID = table.Column<Guid>(nullable: false),
                    CategoryID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyCategory", x => new { x.AgencyID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_AgencyCategory_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyCategory_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyFinancialPlan",
                columns: table => new
                {
                    AgencyID = table.Column<Guid>(nullable: false),
                    FinancialID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyFinancialPlan", x => new { x.AgencyID, x.FinancialID });
                    table.ForeignKey(
                        name: "FK_AgencyFinancialPlan_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyFinancialPlan_FinancialPlan_FinancialID",
                        column: x => x.FinancialID,
                        principalTable: "FinancialPlan",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencyService",
                columns: table => new
                {
                    AgencyID = table.Column<Guid>(nullable: false),
                    ServiceID = table.Column<Guid>(nullable: false),
                    TypicalResponseTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencyService", x => new { x.AgencyID, x.ServiceID });
                    table.ForeignKey(
                        name: "FK_AgencyService_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencyService_Service_ServiceID",
                        column: x => x.ServiceID,
                        principalTable: "Service",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AgencySpecialty",
                columns: table => new
                {
                    AgencyID = table.Column<Guid>(nullable: false),
                    SpecialtyID = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgencySpecialty", x => new { x.AgencyID, x.SpecialtyID });
                    table.ForeignKey(
                        name: "FK_AgencySpecialty_Agencies_AgencyID",
                        column: x => x.AgencyID,
                        principalTable: "Agencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgencySpecialty_Specialty_SpecialtyID",
                        column: x => x.SpecialtyID,
                        principalTable: "Specialty",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgencyCategory_CategoryID",
                table: "AgencyCategory",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyFinancialPlan_FinancialID",
                table: "AgencyFinancialPlan",
                column: "FinancialID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyLocation_AgencyID",
                table: "AgencyLocation",
                column: "AgencyID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencyService_ServiceID",
                table: "AgencyService",
                column: "ServiceID");

            migrationBuilder.CreateIndex(
                name: "IX_AgencySpecialty_SpecialtyID",
                table: "AgencySpecialty",
                column: "SpecialtyID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgencyAgeGroup");

            migrationBuilder.DropTable(
                name: "AgencyCategory");

            migrationBuilder.DropTable(
                name: "AgencyFinancialPlan");

            migrationBuilder.DropTable(
                name: "AgencyLocation");

            migrationBuilder.DropTable(
                name: "AgencyService");

            migrationBuilder.DropTable(
                name: "AgencySpecialty");

            migrationBuilder.DropTable(
                name: "AgeGroup");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropTable(
                name: "FinancialPlan");

            migrationBuilder.DropTable(
                name: "Service");

            migrationBuilder.DropTable(
                name: "Agencies");

            migrationBuilder.DropTable(
                name: "Specialty");
        }
    }
}
