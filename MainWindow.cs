using System;
using Gtk;

public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel) => Build();

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnCalculateClicked(object sender, EventArgs e)
    {
        double width = Convert.ToDouble(value: tb_width.Buffer.Text);
        double height = Convert.ToDouble(value: tb_height.Buffer.Text);
        double dpi = Convert.ToDouble(tb_dpi.Buffer.Text);
        double depth = Convert.ToDouble(tb_colordepth.Buffer.Text);

        double px_width = ((width / 10) / 2.54) * dpi;
        double px_height = ((height / 10) / 2.54) * dpi;

        px_width = Convert.ToInt32(px_width) + 1;
        px_height = Convert.ToInt32(px_height) + 1;
        double px_total = Convert.ToDouble(px_width * px_height);

        double bits = Convert.ToDouble(px_total * depth);
        double bytes = Convert.ToDouble(bits / 8);

        if (cb_unit.ActiveText == "1000 (KB, MB, GB, TB)")
        {
            double KB = Math.Round(bytes / 1000);
            double MB = Math.Round(KB / 1000, 3);
            double GB = Math.Round(MB / 1000, 3);
            double TB = Math.Round(GB / 1000, 3);

            tb_result.Buffer.Text = "**Pixel berechnen**\n\nBreite: " + (width / 10) + " cm / 2,54 * " + dpi + " dpi = " + px_width + " px\nHöhe: " +
                (height / 10) + " cm / 2,54 * " + dpi + " dpi = " + px_height + " px\nGesamt: " +
                px_width + " px * " + px_height + " px = " + px_total + " px" + "\n\n**Dateigröße berechnen**\n\nBit: " +
                px_total + " px * " + depth + " Bit = " + bits + " Bit\nByte: " +
                bits + " / 8 = " + bytes + " Byte\nKbyte: " +
                bytes + " /1000 = " + bytes / 1000 + " KByte\nMByte: " +
                KB + " KB / 1000 = " + MB + " MByte\n";
            if (GB / 1000 > 1)
            {
                tb_result.Buffer.Text += "GByte: " + MB + " MB / 1000 = " + GB + " GByte\n";
                tb_result.Buffer.Text += "TByte: " + GB + " GB / 1000 = " + TB + " TByte\n";
            }
            else if (MB / 1000 > 1)
            {
                tb_result.Buffer.Text += "GByte: " + MB + " MB / 1000 = " + GB + " GByte\n";
            }
        }
        else
        {
            double KB = Math.Round(bytes / 1024);
            double MB = Math.Round(KB / 1024, 3);
            double GB = Math.Round(MB / 1024, 3);
            double TB = Math.Round(GB / 1024, 3);

            tb_result.Buffer.Text = "**Pixel berechnen**\n\nBreite: " + (width / 10) + " cm / 2,54 * " + dpi + " dpi = " + px_width + " px\nHöhe: " +
                (height / 10) + " cm / 2,54 * " + dpi + " dpi = " + px_height + " px\nGesamt: " +
                px_width + " px * " + px_height + " px = " + px_total + " px" + "\n\n" + "**Dateigröße berechnen**" + "\n\nBit: " +
                px_total + " px * " + depth + " Bit = " + bits + " Bit\nByte: " +
                bits + " / 8 = " + bytes + " Byte\nKiB: " +
                bytes + " / 1024 = " + KB + " KiB\nMiB: " +
                KB + " KB / 1024 = " + MB + " MiB\n";
            if (GB / 1000 > 1)
            {
                tb_result.Buffer.Text += "GiB: " + MB + " MiB / 1000 = " + GB + " GiB\n";
                tb_result.Buffer.Text += "TiB: " + GB + " GiB / 1000 = " + TB + " TiB\n";
            }
            else if (MB / 1000 > 1)
            {
                tb_result.Buffer.Text += "GiB: " + MB + " MiB / 1000 = " + GB + " GiB\n";
            }
        }
    }

    protected void OnAbout(object sender, EventArgs e)
    {
        AboutDialog about = new AboutDialog
        {
            ProgramName = "BildrechnerDLX",
            Version = "1.0.0"
        };

        about.Run();

        about.Destroy();
    }


    protected void OnClose(object sender, EventArgs e)
    {

        this.GetDefaultSize(out int width, out int height);
        this.Resize(width, height);

        tb_result.Buffer.Text = "";
        tb_dpi.Buffer.Text = "";
        tb_width.Buffer.Text = "";
        tb_height.Buffer.Text = "";
        tb_colordepth.Buffer.Text = "";


        this.Title = "Bildrechner DLX";
    }

    protected void OnExit(object sender, EventArgs e)
    {
        Application.Quit();
    }
}