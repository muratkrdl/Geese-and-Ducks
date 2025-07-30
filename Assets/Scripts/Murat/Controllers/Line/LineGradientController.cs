using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Murat.Controllers.Line
{
    public class LineGradientController : MonoBehaviour
    {
        private LineRenderer _myLines;
        private Gradient _defaultGradient;
        
        private const float GradientMargin = .03f;

        public void Initialize(LineRenderer lineRenderer, Gradient gradient)
        {
            _myLines = lineRenderer;
            _defaultGradient = gradient;
        }

        public async UniTaskVoid UpdateGradientAsync(int a, int b, System.Func<int> getCurrentLineIndex, System.Action<bool> setReversing)
        {
            setReversing(true);
            while (getCurrentLineIndex() != Mathf.Min(a,b))
            {
                float startTime = (float)a / (_myLines.positionCount - 1);
                float endTime = (float)b / (_myLines.positionCount - 1);

                _myLines.colorGradient = CreateGradient(startTime, endTime);
                await UniTask.Yield();
            }

            _myLines.colorGradient = _defaultGradient;
            setReversing(false);
        }

        private Gradient CreateGradient(float start, float end)
        {
            var gradient = new Gradient();

            var colorKeys = new GradientColorKey[]
            {
                new GradientColorKey(start == 0 ? Color.red : Color.white, 0f),
                new GradientColorKey(start == 0 ? Color.red : Color.white, Mathf.Max(0f, start - GradientMargin)),
                new GradientColorKey(Color.red, start),
                new GradientColorKey(Color.red, end),
                new GradientColorKey(Color.white, Mathf.Min(1f, end + GradientMargin)),
                new GradientColorKey(Color.white, 1f)
            };

            gradient.SetKeys(colorKeys, new[]
            {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            });

            return gradient;
        }
    }
} 