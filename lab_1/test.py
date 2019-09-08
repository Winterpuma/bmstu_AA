import unittest
from main import LevRecursion, LevTable

class TestLevDistanse(unittest.TestCase):

    def setUp(self):
        self.function = LevTable
        
    def testEmpty(self):
        self.assertEqual(self.function("", ""), 0)

    def testSame(self):
        self.assertEqual(self.function("abc", "abc"), 0)
        self.assertEqual(self.function("0", "0"), 0)

    def testDifferent(self):
        self.assertEqual(self.function("a", ""), 1)
        self.assertEqual(self.function("", "1"), 1)
        self.assertEqual(self.function("b", "c"), 1)
        self.assertEqual(self.function("bc", "b"), 1)
        self.assertEqual(self.function("bc", "c"), 1)
        self.assertEqual(self.function("ab", "cd"), 2)

    #def testTypo(self):
        #self.assertEqual(self.function("ac", "ca"), 2)

if __name__ == '__main__':
    unittest.main()
