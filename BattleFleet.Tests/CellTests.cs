using BattleFleet.src.PlayerBoard;

namespace BattleFleet.Tests
{
    [TestClass]
    public sealed class CellTests
    {
        private readonly Cell cell = new Cell();

        [TestMethod]
        public void CreateCellAndCheckInitialValue()
        {
            CellStatus cellStatus = cell.GetCellStatus();

            Assert.AreEqual(CellStatus.EMPTY, cellStatus);
        }

        [TestMethod]
        [DataRow(CellStatus.EMPTY, true)]
        [DataRow(CellStatus.OCCUPIED, true)]
        [DataRow(CellStatus.FORBIDDEN, true)]
        public void UpdateCellAndCheck(CellStatus newStatusCell, bool expectedResult)
        {
            bool result = cell.UpdateCellStatus(newStatusCell);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(CellStatus.HIT, CellStatus.HIT, false)]
        [DataRow(CellStatus.MISS, CellStatus.HIT, true)]
        public void SetCellStatusAndHitCell(CellStatus startStatusCell, CellStatus newStatusCell, bool expectedResult)
        {
            cell.UpdateCellStatus(startStatusCell);

            bool result = cell.UpdateCellStatus(newStatusCell);

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(CellStatus.EMPTY, $"   |")]
        [DataRow(CellStatus.OCCUPIED, $"   |")]
        [DataRow(CellStatus.FORBIDDEN, $"   |")]
        [DataRow(CellStatus.HIT, $" X |")]
        [DataRow(CellStatus.MISS, $" - |")]
        public void SetCellStatusAndGetHideCell(CellStatus startStatusCell, string expectedResult)
        {
            cell.UpdateCellStatus(startStatusCell);

            string result = cell.ToStringHide();

            Assert.AreEqual(expectedResult, result);
        }

        [TestMethod]
        [DataRow(CellStatus.EMPTY, $"   |")]
        [DataRow(CellStatus.OCCUPIED, $" ■ |")]
        [DataRow(CellStatus.FORBIDDEN, $"   |")]
        [DataRow(CellStatus.HIT, $" X |")]
        [DataRow(CellStatus.MISS, $" - |")]
        public void SetCellStatusAndGetCodeCell(CellStatus startStatusCell, string expectedResult)
        {
            cell.UpdateCellStatus(startStatusCell);

            string result = cell.ToString();

            Assert.AreEqual(expectedResult, result);
        }
    }
}