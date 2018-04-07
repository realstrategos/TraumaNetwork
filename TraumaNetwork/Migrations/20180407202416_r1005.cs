using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TraumaNetwork.Migrations
{
    public partial class r1005 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryID",
                table: "AgencyLocation",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_AgencyLocation_CategoryID",
                table: "AgencyLocation",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_AgencyLocation_Category_CategoryID",
                table: "AgencyLocation",
                column: "CategoryID",
                principalTable: "Category",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AgencyLocation_Category_CategoryID",
                table: "AgencyLocation");

            migrationBuilder.DropIndex(
                name: "IX_AgencyLocation_CategoryID",
                table: "AgencyLocation");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "AgencyLocation");
        }
    }
}
