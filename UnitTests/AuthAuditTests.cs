using System;
using Modulo_de_autorizacao_e_autenticacao.Models;
using Xunit;

namespace UnitTests
{
    public class AuthAuditTests
    {
        [Fact]
        public void AuthAudit_ShouldInitializeCorrectly()
        {
            var id = 1L;
            var createdAt = DateTimeOffset.Now;
            var authId = Guid.NewGuid();
            var auditLog = "Test log";

            var authAudit = new AuthAudit
            {
                id = id,
                created_at = createdAt,
                auth_id = authId,
                audit_log = auditLog
            };

            Assert.Equal(id, authAudit.id);
            Assert.Equal(createdAt, authAudit.created_at);
            Assert.Equal(authId, authAudit.auth_id);
            Assert.Equal(auditLog, authAudit.audit_log);
        }

        [Fact]
        public void AuthAudit_Equals_ShouldReturnTrueForEqualObjects()
        {
            var id = 1L;
            var createdAt = DateTimeOffset.Now;
            var authId = Guid.NewGuid();
            var auditLog = "Test log";

            var authAudit1 = new AuthAudit
            {
                id = id,
                created_at = createdAt,
                auth_id = authId,
                audit_log = auditLog
            };

            var authAudit2 = new AuthAudit
            {
                id = id,
                created_at = createdAt,
                auth_id = authId,
                audit_log = auditLog
            };

            Assert.True(authAudit1.Equals(authAudit2));
        }

        [Fact]
        public void AuthAudit_GetHashCode_ShouldReturnSameHashCodeForEqualObjects()
        {
            var id = 1L;
            var createdAt = DateTimeOffset.Now;
            var authId = Guid.NewGuid();
            var auditLog = "Test log";

            var authAudit1 = new AuthAudit
            {
                id = id,
                created_at = createdAt,
                auth_id = authId,
                audit_log = auditLog
            };

            var authAudit2 = new AuthAudit
            {
                id = id,
                created_at = createdAt,
                auth_id = authId,
                audit_log = auditLog
            };

            Assert.Equal(authAudit1.GetHashCode(), authAudit2.GetHashCode());
        }
    }
}
