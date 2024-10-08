using Xunit;
using Modulo_de_autorizacao_e_autenticacao.Models;

namespace UnitTests
{
    public class AuthAuditTests
    {
        [Fact]
        public void AuthAudit_CreatesObjectWithDefaultValues()
        {
            // Act
            var authAudit = new AuthAudit();

            // Assert
            Assert.Equal(0, authAudit.id);
            Assert.Equal(default(DateTimeOffset), authAudit.created_at);
            Assert.Equal(default(Guid), authAudit.auth_id);
            Assert.Null(authAudit.audit_log);
        }

        [Fact]
        public void AuthAudit_CanSetAllProperties()
        {
            // Arrange
            var authAudit = new AuthAudit();
            long expectedId = 1;
            DateTimeOffset expectedCreatedAt = DateTimeOffset.UtcNow;
            Guid expectedAuthId = Guid.NewGuid();
            string expectedAuditLog = "Login successful";

            // Act
            authAudit.id = expectedId;
            authAudit.created_at = expectedCreatedAt;
            authAudit.auth_id = expectedAuthId;
            authAudit.audit_log = expectedAuditLog;

            // Assert
            Assert.Equal(expectedId, authAudit.id);
            Assert.Equal(expectedCreatedAt, authAudit.created_at);
            Assert.Equal(expectedAuthId, authAudit.auth_id);
            Assert.Equal(expectedAuditLog, authAudit.audit_log);
        }

        [Fact]
        public void AuthAudit_ObjectsWithSameProperties_AreEqual()
        {
            // Arrange
            var authAudit1 = new AuthAudit
            {
                id = 1,
                created_at = DateTimeOffset.UtcNow,
                auth_id = Guid.NewGuid(),
                audit_log = "Audit log entry"
            };
            var authAudit2 = new AuthAudit
            {
                id = authAudit1.id,
                created_at = authAudit1.created_at,
                auth_id = authAudit1.auth_id,
                audit_log = authAudit1.audit_log
            };

            // Act & Assert
            Assert.Equal(authAudit1, authAudit2);
        }

        [Fact]
        public void AuthAudit_ObjectsWithDifferentProperties_AreNotEqual()
        {
            // Arrange
            var authAudit1 = new AuthAudit
            {
                id = 1,
                created_at = DateTimeOffset.UtcNow,
                auth_id = Guid.NewGuid(),
                audit_log = "Log 1"
            };
            var authAudit2 = new AuthAudit
            {
                id = 2,
                created_at = DateTimeOffset.UtcNow.AddDays(-1),
                auth_id = Guid.NewGuid(),
                audit_log = "Log 2"
            };

            // Act & Assert
            Assert.NotEqual(authAudit1, authAudit2);
        }

        [Fact]
        public void AuthAudit_CanChangeAuditLogProperty()
        {
            // Arrange
            var authAudit = new AuthAudit { audit_log = "Initial Log" };
            string newAuditLog = "Updated Log";

            // Act
            authAudit.audit_log = newAuditLog;

            // Assert
            Assert.Equal(newAuditLog, authAudit.audit_log);
        }
    }
}
