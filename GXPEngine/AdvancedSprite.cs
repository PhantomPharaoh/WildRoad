using GXPEngine;
using GXPEngine.Core;


public class AdvancedSprite : Sprite
{
	protected float _offsetX = 0;
	protected float _offsetY = 0;

	private float[] area;

	public AdvancedSprite(string filename, float[] area) : base(filename, false){
		texture.wrap = true;
		
		this.area = area;
	}

	public void SetOffset(float x, float y){
		_offsetX = x;
		_offsetY = y;

		setUVs();
	}

	public void AddOffset(float x, float y){
		_offsetX += x;
		_offsetY += y;

		setUVs();
	}

	protected override void setUVs(){
		float left = _mirrorX ? 1.0f : 0.0f;
		float right = _mirrorX ? 0.0f : 1.0f;
		float top = _mirrorY ? 1.0f : 0.0f;
		float bottom = _mirrorY ? 0.0f : 1.0f;

		left += _offsetX;
		right += _offsetX;
		top += _offsetY;
		bottom += _offsetY;

		_uvs = new float[8] { left, top, right, top, right, bottom, left, bottom };
	}

	protected override void RenderSelf(GLContext glContext){
		_texture.Bind();

		glContext.SetColor(
			(byte)((_color >> 16) & 0xFF),
			(byte)((_color >> 8) & 0xFF),
			(byte)(_color & 0xFF),
			(byte)(_alpha * 0xFF));
		glContext.DrawQuad(area, _uvs);

		_texture.Unbind();
	}
}
