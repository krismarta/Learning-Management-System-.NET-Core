using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineCourseAPI.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tb_m_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_faq",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_faq", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_role",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_role", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_user",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    gender = table.Column<int>(type: "int", nullable: false),
                    birthDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_user_number",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_user_number", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_account",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Roleid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_role_Roleid",
                        column: x => x.Roleid,
                        principalTable: "tb_m_role",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_account_tb_m_user_id",
                        column: x => x.id,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_bank_account",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    no = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    holder_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    bank_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_bank_account", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_bank_account_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    thumbnail = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Categoryid = table.Column<int>(type: "int", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_course_tb_m_category_Categoryid",
                        column: x => x.Categoryid,
                        principalTable: "tb_m_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_course_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_review_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rate = table.Column<int>(type: "int", nullable: false),
                    review = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    date_review = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_review_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_review_course_tb_m_course_Courseid",
                        column: x => x.Courseid,
                        principalTable: "tb_m_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_m_review_course_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_sub_course",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    duration = table.Column<int>(type: "int", nullable: false),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_sub_course", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_sub_course_tb_m_course_Courseid",
                        column: x => x.Courseid,
                        principalTable: "tb_m_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_mycourse",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_mycourse", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_t_mycourse_tb_m_course_Courseid",
                        column: x => x.Courseid,
                        principalTable: "tb_m_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_mycourse_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_payment",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_request = table.Column<DateTime>(type: "datetime2", nullable: false),
                    date_accept = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_payment", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_t_payment_tb_m_course_Courseid",
                        column: x => x.Courseid,
                        principalTable: "tb_m_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_payment_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_t_payment_midtrans",
                columns: table => new
                {
                    id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    method = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<int>(type: "int", nullable: false),
                    VA = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Userid = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Courseid = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_t_payment_midtrans", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_t_payment_midtrans_tb_m_course_Courseid",
                        column: x => x.Courseid,
                        principalTable: "tb_m_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tb_t_payment_midtrans_tb_m_user_Userid",
                        column: x => x.Userid,
                        principalTable: "tb_m_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tb_m_sub_course_log",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    date_finished = table.Column<DateTime>(type: "datetime2", nullable: false),
                    status = table.Column<int>(type: "int", nullable: false),
                    MyCourseid = table.Column<int>(type: "int", nullable: false),
                    SubCourseid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tb_m_sub_course_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_tb_m_sub_course_log_tb_m_sub_course_SubCourseid",
                        column: x => x.SubCourseid,
                        principalTable: "tb_m_sub_course",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tb_m_sub_course_log_tb_t_mycourse_MyCourseid",
                        column: x => x.MyCourseid,
                        principalTable: "tb_t_mycourse",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_account_Roleid",
                table: "tb_m_account",
                column: "Roleid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_bank_account_Userid",
                table: "tb_m_bank_account",
                column: "Userid",
                unique: true,
                filter: "[Userid] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_course_Categoryid",
                table: "tb_m_course",
                column: "Categoryid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_course_Userid",
                table: "tb_m_course",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_review_course_Courseid",
                table: "tb_m_review_course",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_review_course_Userid",
                table: "tb_m_review_course",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sub_course_Courseid",
                table: "tb_m_sub_course",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sub_course_log_MyCourseid",
                table: "tb_m_sub_course_log",
                column: "MyCourseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_m_sub_course_log_SubCourseid",
                table: "tb_m_sub_course_log",
                column: "SubCourseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_mycourse_Courseid",
                table: "tb_t_mycourse",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_mycourse_Userid",
                table: "tb_t_mycourse",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_payment_Courseid",
                table: "tb_t_payment",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_payment_Userid",
                table: "tb_t_payment",
                column: "Userid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_payment_midtrans_Courseid",
                table: "tb_t_payment_midtrans",
                column: "Courseid");

            migrationBuilder.CreateIndex(
                name: "IX_tb_t_payment_midtrans_Userid",
                table: "tb_t_payment_midtrans",
                column: "Userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tb_m_account");

            migrationBuilder.DropTable(
                name: "tb_m_bank_account");

            migrationBuilder.DropTable(
                name: "tb_m_faq");

            migrationBuilder.DropTable(
                name: "tb_m_review_course");

            migrationBuilder.DropTable(
                name: "tb_m_sub_course_log");

            migrationBuilder.DropTable(
                name: "tb_t_payment");

            migrationBuilder.DropTable(
                name: "tb_t_payment_midtrans");

            migrationBuilder.DropTable(
                name: "tb_t_user_number");

            migrationBuilder.DropTable(
                name: "tb_m_role");

            migrationBuilder.DropTable(
                name: "tb_m_sub_course");

            migrationBuilder.DropTable(
                name: "tb_t_mycourse");

            migrationBuilder.DropTable(
                name: "tb_m_course");

            migrationBuilder.DropTable(
                name: "tb_m_category");

            migrationBuilder.DropTable(
                name: "tb_m_user");
        }
    }
}
