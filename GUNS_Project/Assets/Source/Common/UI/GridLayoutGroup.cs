using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Mathf;

namespace Source
{
    public class GridLayoutGroup : LayoutGroup
    {
        private enum FitType
        {
            Uniform,
            With,
            Height,
            FixedRows,
            FixedColumns
        }

        [SerializeField] private FitType _fitType;

        [SerializeField] private int _rows;
        [SerializeField] private int _columns;

        [SerializeField] private Vector2 _cellSize;
        [SerializeField] private Vector2 _spacing;

        [SerializeField] private bool _isFitX = false;
        [SerializeField] private bool _isFitY = false;

        public override void CalculateLayoutInputHorizontal()
        {
            base.CalculateLayoutInputHorizontal();

            int childCount = transform.childCount;

            if (_fitType == FitType.With || _fitType == FitType.Height || _fitType == FitType.Uniform)
            {
                _isFitX = true;
                _isFitY = true;

                float sqrtOfChildCount = Sqrt(childCount);

                _rows = CeilToInt(sqrtOfChildCount);
                _columns = CeilToInt(sqrtOfChildCount);
            }

            if (_fitType == FitType.With || _fitType == FitType.FixedColumns)
                _rows = CeilToInt(childCount / (float) _columns);

            else if (_fitType == FitType.Height || _fitType == FitType.FixedRows)
                _columns = CeilToInt(childCount / (float) _rows);

            Rect rect = rectTransform.rect;
            float parentWith = rect.width;
            float parentHeight = rect.height;

            float columnsWith = (parentWith / (float) _columns);
            float rowsHeight = (parentHeight / (float) _rows);

            float spacingsWith = ((_spacing.x / (float) _columns) * (_columns - 1));
            float spacingsHeight = ((_spacing.y / (float) _rows) * (_rows - 1));

            float paddingsWith = (padding.left + padding.right) / (float) _columns;
            float paddingsHeight = (padding.top + padding.bottom) / (float) _rows;

            float cellWith = columnsWith - spacingsWith - paddingsWith;
            float cellHeight = rowsHeight - spacingsHeight - paddingsHeight;

            _cellSize.x = _isFitX ? cellWith : _cellSize.x;
            _cellSize.y = _isFitY ? cellHeight : _cellSize.y;

            float totalWidth = _columns * _cellSize.x
                             + Mathf.Max(0, _columns - 1) * _spacing.x
                             + padding.horizontal;

            SetLayoutInputForAxis(
                0,
                totalWidth,
                _isFitX ? 1 : 0,
                0
            );

            int childrenCount = rectChildren.Count;

            for (int i = 0; i < childrenCount; i++)
            {
                int rowNumber = i / _columns;
                int columnNumber = i % _columns;

                RectTransform child = rectChildren[i];

                float sizeX = (_cellSize.x * columnNumber);
                float sizeY = (_cellSize.y * rowNumber);

                float spacingX = (_spacing.x * columnNumber);
                float spacingY = (_spacing.y * rowNumber);

                float positionX = sizeX + spacingX + padding.left;
                float positionY = sizeY + spacingY + padding.top;

                SetChildAlongAxis(child, 0, positionX, _cellSize.x);
                SetChildAlongAxis(child, 1, positionY, _cellSize.y);
            }
        }

        public override void CalculateLayoutInputVertical()
        {
            int childCount = transform.childCount;

            if (_fitType == FitType.Height || _fitType == FitType.FixedRows)
                _columns = Mathf.CeilToInt(childCount / (float) _rows);

            Rect rect = rectTransform.rect;
            float parentHeight = rect.height;

            float rowsHeight = parentHeight / _rows;
            float spacingsHeight = (_spacing.y * (_rows - 1));
            float paddingsHeight = (float) padding.vertical / _rows;

            _cellSize.y = _isFitY ? (rowsHeight - spacingsHeight - paddingsHeight) : _cellSize.y;

            float totalHeight = _rows * _cellSize.y
                              + Mathf.Max(0, _rows - 1) * _spacing.y
                              + padding.vertical;

            SetLayoutInputForAxis(
                totalHeight,
                totalHeight,
                _isFitY ? 0 : 1,
                1
            );
        }
        protected void SetChildrenSizes()
        {
            int childrenCount = rectChildren.Count;
    
            for (int i = 0; i < childrenCount; i++)
            {
                int rowNumber = i / _columns;
                int columnNumber = i % _columns;

                RectTransform child = rectChildren[i];

                float positionX = (_cellSize.x + _spacing.x) * columnNumber + padding.left;
                float positionY = (_cellSize.y + _spacing.y) * rowNumber + padding.top;

                SetChildAlongAxis(child, 0, positionX, _cellSize.x);
                SetChildAlongAxis(child, 1, positionY, _cellSize.y);
                
                LayoutRebuilder.MarkLayoutForRebuild(rectChildren[i]);
            }
        }

        public override void SetLayoutHorizontal()
        {
            CalculateLayoutInputHorizontal();
            SetChildrenSizes();
        }

        public override void SetLayoutVertical()
        {
            CalculateLayoutInputVertical();
            SetChildrenSizes();
        }
    }
}